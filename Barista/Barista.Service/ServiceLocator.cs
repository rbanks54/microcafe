using Barista.Service.MicroServices.Orders.Handlers;
using MicroServices.Common.MessageBus;

namespace Barista.Service
{
    public static class ServiceLocator
    {
        public static IMessageBus Bus { get; set; }
        public static OrderCommandHandlers OrderCommands { get; set; }
    }
}