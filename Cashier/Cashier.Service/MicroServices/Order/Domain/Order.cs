using System;
using System.Linq;
using Cashier.Common.Events;
using MicroServices.Common;

namespace Cashier.Service.MicroServices.Brand.Domain
{
    public class Order : Aggregate
    {
        private Order()
        {
        }

        public Order(Guid id, Guid productId, int quantity)
        {
            if (quantity <= 0) throw new ArgumentOutOfRangeException("quantity", "quantity must be a number from 1 and up");
            if (Guid.Empty.Equals(productId)) throw new ArgumentNullException("productId", "A valid Product Guid must be provided");

            ApplyEvent(new OrderPlaced(id, productId, quantity));
        }

        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public bool HasBeenPaid { get; private set; }

        private void Apply(OrderPlaced e)
        {
            Id = e.Id;
            ProductId = e.ProductId;
            Quantity = e.Quantity;
            HasBeenPaid = false;
        }

        private void Apply(OrderPaidFor e)
        {
            HasBeenPaid = true;
        }

        public void PayForOrder(int originalVersion)
        {
            //can only update the current version of an aggregate
            ValidateVersion(originalVersion);

            ApplyEvent(new OrderPaidFor(Id));
        }

        private void ValidateVersion(int version)
        {
            if (Version != version)
            {
                throw new ArgumentOutOfRangeException("version", "Invalid version specified: the version is out of sync.");
            }
        }
    }
}