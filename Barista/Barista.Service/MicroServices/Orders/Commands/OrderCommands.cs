using System;
using MicroServices.Common;

namespace Barista.Service.MicroServices.Orders.Commands
{
    public class CompleteOrder : ICommand
    {
        public Guid Id { get; private set; }
        public int OriginalVersion { get; private set; }

        public CompleteOrder(Guid id, int originalVersion)
        {
            Id = id;
            OriginalVersion = originalVersion;
        }
    }

    public class CancelOrder: ICommand
    {
        public Guid Id { get; private set; }
        public int OriginalVersion { get; private set; }

        public CancelOrder(Guid id, int originalVersion)
        {
            Id = id;
            OriginalVersion = originalVersion;
        }
    }
}
