using System;
using Newtonsoft.Json;
using MicroServices.Common;

namespace Barista.Common.Events
{
    public class OrderPrepared : Event
    {
        public OrderPrepared(Guid id)
        {
            Id = id;
        }

        [JsonConstructor]
        private OrderPrepared(Guid id, int version) : this(id)
        {
            Version = version;
        }

        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
    }
}