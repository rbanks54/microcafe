using System;
using MicroServices.Common;

namespace Cashier.Service.MicroServices.Brand.Commands
{
    public class PlaceOrder : ICommand
    {
        public PlaceOrder(Guid id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
    }

    public class CancelOrder : ICommand
    { }

    public class PayForOrder : ICommand
    { }

}