using System;
using System.Collections.Generic;

namespace MicroServices.Common
{
    public abstract class Aggregate 
    {
        internal readonly List<Event> events = new List<Event>();

        public Guid Id { get; protected set; }

        private int version = -1;
        public int Version { get { return version; } internal set { version = value; } }

        public IEnumerable<Event> GetUncommittedEvents()
        {
            return events;
        }

        public void MarkEventsAsCommitted()
        {
            events.Clear();
        }

        public void LoadStateFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history) ApplyEvent(e, false);
        }

        protected internal void ApplyEvent(Event @event)
        {
            ApplyEvent(@event, true);
        }

        protected virtual void ApplyEvent(Event @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
            if (isNew)
            {
                @event.Version = ++Version;
                events.Add(@event);
            }
            else
            {
                Version = @event.Version;
            }
        }
    }

    public abstract class ReadModelAggregate : Aggregate
    {
        protected internal void ApplyEvent(Event @event, int version)
        {
            @event.Version = version;
            ApplyEvent(@event, true);
        }
        protected override void ApplyEvent(Event @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
            if (isNew)
            {
                events.Add(@event);
            }
            Version++;

        }
    }
}