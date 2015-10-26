using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MicroServices.Common.Repository
{
    public class RedisReadModelRepository<T>
        : IReadModelRepository<T> where T : ReadObject
    {
        private readonly IDatabase database;

        public RedisReadModelRepository(IDatabase database)
        {
            this.database = database;
        }

        public IEnumerable<T> GetAll()
        {
            var get = new RedisValue[] { InstanceName() + "*" };
            var result = database.SortAsync(SetName(), sortType: SortType.Alphabetic, by: "nosort", get: get).Result;

            var readObjects = result.Select(v => JsonConvert.DeserializeObject<T>(v)).AsEnumerable();
            return readObjects;
        }

        public T Get(Guid id)
        {
            var key = Key(id);
            var result = database.StringGetAsync(key).Result;
            var dto = JsonConvert.DeserializeObject<T>(result);
            return dto;
        }

        public void Update(T t)
        {
            var key = Key(t.Id);
            var serialised = JsonConvert.SerializeObject(t);
            database.StringSetAsync(key, serialised, when: When.Exists);
        }

        public void Insert(T t)
        {
            var serialised = JsonConvert.SerializeObject(t);
            var key = Key(t.Id);
            var transaction = database.CreateTransaction();
            transaction.StringSetAsync(key, serialised);
            transaction.SetAddAsync(SetName(), t.Id.ToString("N"));
            var committed = transaction.ExecuteAsync().Result;
            if (!committed)
            {
                throw new ApplicationException("transaction failed. Now what?");
            }
        }

        private string Key(Guid id)
        {
            return InstanceName() + id.ToString("N");
        }

        private string InstanceName()
        {
            var type = typeof (T);
            return string.Format("{0}:", type.FullName);
        }
        private string SetName()
        {
            return string.Format("{0}Set", InstanceName()); 
        }


    }
}