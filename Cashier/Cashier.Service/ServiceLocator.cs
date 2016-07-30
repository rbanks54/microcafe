using Cashier.Service.MicroServices.Order.Handlers;
using MicroServices.Common.MessageBus;

namespace Cashier.Service
{
    public static class ServiceLocator
    {
        public static IMessageBus Bus { get; set; }
        public static OrderCommandHandlers OrderCommands { get; set; }
    }
}