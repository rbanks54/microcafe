using System;
using System.Collections.Generic;

namespace MicroServices.Common.Repository
{
    public interface IReadModelRepository<T>
        where T : ReadObject
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Update(T t);
        void Insert(T t);
    }
}