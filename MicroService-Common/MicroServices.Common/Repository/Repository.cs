using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MicroServices.Common.Repository
{
    public abstract class Repository : IRepository
    {
        public abstract void Save<TAggregate>(TAggregate aggregate) where TAggregate : Aggregate;
        public abstract TAggregate GetById<TAggregate>(Guid id) where TAggregate : Aggregate;
        public abstract Task SaveAsync<TAggregate>(TAggregate aggregate) where TAggregate : Aggregate;
        public abstract Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : Aggregate;

        protected int CalculateExpectedVersion<T>(Aggregate aggregate, List<T> events)
        {
            var expectedVersion = aggregate.Version - events.Count;
            return expectedVersion;
        }

        protected TAggregate BuildAggregate<TAggregate>(IEnumerable<Event> events) where TAggregate : Aggregate
        {
            var result = (TAggregate)Activator.CreateInstance(typeof(TAggregate), true);
            result.LoadStateFromHistory(events);
            return result;
        }
    }
}