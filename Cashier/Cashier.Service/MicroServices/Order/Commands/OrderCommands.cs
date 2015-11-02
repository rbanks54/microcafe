using System;
using MicroServices.Common;

namespace Cashier.Service.MicroServices.Brand.Commands
{
    public class PlaceOrder : ICommand
    {
        public PlaceOrder(Guid id, Guid productId, int quantity)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
    }

    public class PayForOrder : ICommand
    {
        public PayForOrder(Guid id, int version)
        {
            Id = id;
            Version = version;
        }

        public Guid Id { get; private set; }
        public int Version { get; set; }
    }

}