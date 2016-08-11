using System;

using MicroServices.Common;

namespace Barista.Service.MicroServices.Orders.Domain
{
    public class Order : Aggregate
    {
        private Order() { }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsCompleted { get; private set; }
                
        private void Apply(OrderPlaced o)
        {
            Id = o.Id;
            Code = o.Code;
            Name = o.Name;
        }

        private void Apply(OrderCompleted c)
        {
            IsCompleted = true;
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
