using System;

namespace Cashier.Service.DataTransferObjects.Commands
{
    public class PlaceOrderCommand
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quanity { get; set; }
    }

    public class PayForOrderCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}