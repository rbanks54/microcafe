using System;

using MicroServices.Common;
using Cashier.Common.Events;
using Barista.Common.Events;

namespace Barista.Service.MicroServices.Orders.Domain
{
    public class Order : Aggregate
    {
        private Order() { }

        public Guid ProductId { get; private set; }
        public bool IsCompleted { get; private set; }
                
        private void Apply(OrderPlaced o)
        {
            Id = o.Id;
            ProductId = o.ProductId;
        }

        private void Apply(OrderPrepared c)
        {
            IsCompleted = true;
        }

        public void CompletePreparation(int originalVersion)
        {
            //can only update the current version of an aggregate
            ValidateVersion(originalVersion);

            ApplyEvent(new OrderPrepared(Id));
        }

        void ValidateVersion(int version)
        {
            if (Version != version)
            {
                throw new ArgumentOutOfRangeException("version", "Invalid version specified: the version is out of sync.");
            }
        }
    }
}
