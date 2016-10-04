using System;
using Newtonsoft.Json;
using MicroServices.Common;

namespace Barista.Common.Events
{
    //This mimics the cashier orderplaced event, but exists so that 
    //we know this is an event that occurred in the Barista domain
    public class OrderPlaced : Event
    {
        public OrderPlaced(Guid id, Guid productId, int quantity)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
        }

        [JsonConstructor]
        private OrderPlaced(Guid id, Guid productId, int quantity, int version) : this(id, productId, quantity)
        {
            Version = version;
        }

        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
    }

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