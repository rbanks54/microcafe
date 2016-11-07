using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using MicroServices.Common.MessageBus;
using System.Threading.Tasks;
using MicroServices.Common.Exceptions;

namespace MicroServices.Common.Repository
{
    public class InMemoryRepository : Repository
    {
        private readonly IMessageBus bus;
        public Dictionary<Guid, List<string>> EventStore = new Dictionary<Guid, List<string>>();
        private readonly List<Event> latestEvents = new List<Event>();
        private readonly JsonSerializerSettings serializationSettings;

        public InMemoryRepository(IMessageBus bus)
        {
            this.bus = bus;
            serializationSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }

        public override Task SaveAsync<TAggregate>(TAggregate aggregate)
        {
            throw new NotImplementedException();
        }

        public override Task<TAggregate> GetByIdAsync<TAggregate>(Guid id)
        {
            throw new NotImplementedException();
        }


        public override void Save<TAggregate>(TAggregate aggregate)
        {
            var eventsToSave = aggregate.GetUncommittedEvents().ToList();
            var serializedEvents = eventsToSave.Select(Serialize).ToList();
            var expectedVersion = CalculateExpectedVersion(aggregate, eventsToSave);
            if (expectedVersion < 0)
            {
                EventStore.Add(aggregate.Id, serializedEvents);
            }
            else
            {
                var existingEvents = EventStore[aggregate.Id];
                var currentversion = existingEvents.Count - 1;
                if (currentversion != expectedVersion)
                {
                    throw new AggregateVersionException(aggregate.Id, typeof(TAggregate), currentversion, expectedVersion);
                }
                existingEvents.AddRange(serializedEvents);
            }
            latestEvents.AddRange(eventsToSave);
            if (bus != null)
            {
                for (var index = latestEvents.Count - 1; index >= 0; index--)
                {
                    var latestEvent = latestEvents[index];
                    bus.Publish(latestEvent);
                    // Add any logic here to check if things worked before removing the event from the store.
                    latestEvents.RemoveAt(index);
                }
            }
            aggregate.MarkEventsAsCommitted();
        }

        private string Serialize(Event arg)
        {
            return JsonConvert.SerializeObject(arg, serializationSettings);
        }

        public IEnumerable<Event> GetLatestEvents()
        {
            return latestEvents;
        }

        public override TAggregate GetById<TAggregate>(Guid id)
        {
            if (EventStore.ContainsKey(id))
            {
                var events = EventStore[id];
                var deserializedEvents = events.Select(e => JsonConvert.DeserializeObject(e, serializationSettings) as Event);
                return BuildAggregate<TAggregate>(deserializedEvents);
            }

            throw new AggregateNotFoundException(id, typeof(TAggregate));
        }

        public void AddEvents(Dictionary<Guid, IEnumerable<Event>> eventsForAggregates)
        {
            foreach (var eventsForAggregate in eventsForAggregates)
            {
                EventStore.Add(eventsForAggregate.Key, eventsForAggregate.Value.Select(Serialize).ToList());
            }
        }
    }
}
