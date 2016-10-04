using Barista.Service.MicroServices.Orders.Handlers;
using MicroServices.Common.MessageBus;

namespace Barista.Service
{
    public static class ServiceLocator
    {
        public static IMessageBus Bus { get; set; }
        public static BaristaOrderCommandHandlers OrderCommands { get; set; }
        public static CashierOrderEventHandler CahierEventHandler { get; set; }
    }
}