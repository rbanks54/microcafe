using System;
using System.Threading.Tasks;

namespace MicroServices.Common.Repository
{
    public interface IRepository
    {
        void Save<TAggregate>(TAggregate aggregate) where TAggregate : Aggregate;
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : Aggregate;
        Task SaveAsync<TAggregate>(TAggregate aggregate) where TAggregate : Aggregate;
        Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : Aggregate;
    }
}