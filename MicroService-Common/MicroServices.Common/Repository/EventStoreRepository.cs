using EventStore.ClientAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MicroServices.Common.Exceptions;
using MicroServices.Common.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Common.Repository
{
    public class EventStoreRepository : IRepository
    {
        private const string EventClrTypeHeader = "EventClrTypeName";
        private const string AggregateClrTypeHeader = "AggregateClrTypeName";
        private const string CommitIdHeader = "CommitId";
        private const int WritePageSize = 500;
        private const int ReadPageSize = 500;

        private static readonly JsonSerializerSettings serializationSettings;
        private readonly IEventStoreConnection eventStoreConnection;
        private readonly IMessageBus bus;

        static EventStoreRepository()
        {
            serializationSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None
            };
        }

        public EventStoreRepository(IEventStoreConnection eventStoreConnection, IMessageBus bus)
        {
            this.eventStoreConnection = eventStoreConnection;
            this.bus = bus;
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : Aggregate
        {
            return GetByIdAsync<TAggregate>(id).Result;
        }

        public async Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : Aggregate
        {
            return await GetByIdAsync<TAggregate>(id, int.MaxValue);
        }

        public async Task<TAggregate> GetByIdAsync<TAggregate>(Guid id, int version) where TAggregate : Aggregate
        {
            if (version <= 0)
                throw new InvalidOperationException("Cannot get version <= 0");

            var streamName = AggregateIdToStreamName(typeof(TAggregate), id);
            var aggregate = ConstructAggregate<TAggregate>();

            var sliceStart = 0;
            StreamEventsSlice currentSlice;
            do
            {
                var sliceCount = sliceStart + ReadPageSize <= version
                                    ? ReadPageSize
                                    : version - sliceStart + 1;

                currentSlice = await eventStoreConnection.ReadStreamEventsForwardAsync(streamName, sliceStart, sliceCount, false);

                if (currentSlice.Status == SliceReadStatus.StreamNotFound)
                    throw new AggregateNotFoundException(id, typeof(TAggregate));

                if (currentSlice.Status == SliceReadStatus.StreamDeleted)
                    throw new AggregateDeletedException(id, typeof(TAggregate));

                sliceStart = currentSlice.NextEventNumber;

                var history = new List<Event>();
                foreach (var evnt in currentSlice.Events)
                    history.Add(DeserializeEvent(evnt.OriginalEvent.Metadata, evnt.OriginalEvent.Data));
                aggregate.LoadStateFromHistory(history);
            } while (version >= currentSlice.NextEventNumber && !currentSlice.IsEndOfStream);

            if (aggregate.Version != version && version < Int32.MaxValue)
                throw new AggregateVersionException(id, typeof(TAggregate), aggregate.Version, version);

            return aggregate;
        }

        private static TAggregate ConstructAggregate<TAggregate>()
        {
            return (TAggregate)Activator.CreateInstance(typeof(TAggregate), true);
        }

        private static Event DeserializeEvent(byte[] metadata, byte[] data)
        {
            var metadataString = Encoding.UTF8.GetString(metadata);
            var eventClrTypeName = JObject.Parse(metadataString).Property(EventClrTypeHeader).Value;
            var @event = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType((string)eventClrTypeName));
            if (@event as Event == null)
            {
                throw new EventDeserializationException((string)eventClrTypeName, metadataString);
            }
            return @event as Event;
        }

        public void Save<TAggregate>(TAggregate aggregate) where TAggregate : Aggregate
        {
            SaveAsync(aggregate).Wait();
        }

        public async Task SaveAsync<TAggregate>(TAggregate aggregate) where TAggregate : Aggregate
        {
            var commitHeaders = new Dictionary<string, object>
            {
                {CommitIdHeader, aggregate.Id},
                {AggregateClrTypeHeader, aggregate.GetType().AssemblyQualifiedName}
            };

            var streamName = AggregateIdToStreamName(aggregate.GetType(), aggregate.Id);
            var eventsToPublish = aggregate.GetUncommittedEvents();
            var newEvents = eventsToPublish.Cast<object>().ToList();
            var originalVersion = aggregate.Version - newEvents.Count;
            var expectedVersion = originalVersion == -1 ? ExpectedVersion.NoStream : originalVersion;
            var eventsToSave = newEvents.Select(e => ToEventData(Guid.NewGuid(), e, commitHeaders)).ToList();

            if (eventsToSave.Count < WritePageSize)
            {
                await eventStoreConnection.AppendToStreamAsync(streamName, expectedVersion, eventsToSave);
            }
            else
            {
                var transaction = await eventStoreConnection.StartTransactionAsync(streamName, expectedVersion);

                var position = 0;
                while (position < eventsToSave.Count)
                {
                    var pageEvents = eventsToSave.Skip(position).Take(WritePageSize);
                    await transaction.WriteAsync(pageEvents);
                    position += WritePageSize;
                }

                await transaction.CommitAsync();
            }

            if (bus != null)
            {
                foreach (var e in eventsToPublish)
                {
                    bus.Publish(e);
                }
            }

            aggregate.MarkEventsAsCommitted();
        }

        private string AggregateIdToStreamName(Type type, Guid id)
        {
            //Ensure first character of type name is lower case to follow javascript naming conventions
            return string.Format("{0}-{1}", char.ToLower(type.Name[0]) + type.Name.Substring(1), id.ToString("N"));
        }

        private static EventData ToEventData(Guid eventId, object @event, IDictionary<string, object> headers)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event, serializationSettings));

            var eventHeaders = new Dictionary<string, object>(headers)
            {
                {
                    EventClrTypeHeader, @event.GetType().AssemblyQualifiedName
                }
            };
            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventHeaders, serializationSettings));
            var typeName = @event.GetType().Name;

            return new EventData(eventId, typeName, true, data, metadata);
        }

    }
}