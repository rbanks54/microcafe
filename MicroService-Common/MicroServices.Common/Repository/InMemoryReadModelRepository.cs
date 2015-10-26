using System;
using System.Collections.Generic;

namespace MicroServices.Common.Repository
{
    public class InMemoryReadModelRepository<T> 
        : IReadModelRepository<T> where T : ReadObject
    {
        private readonly Dictionary<Guid,T> items = new Dictionary<Guid, T>(); 
        public T Get(Guid id)
        {
            return items[id];
        }

        public IEnumerable<T> GetAll()
        {
            return items.Values;
        }

        public void Insert(T t)
        {
            items.Add(t.Id, t);
        }

        public void Update(T t)
        {
            items[t.Id] = t;
        }
    }
}