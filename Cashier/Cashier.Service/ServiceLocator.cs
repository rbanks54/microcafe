using Cashier.Service.MicroServices.Brand.Handlers;
using MicroServices.Common.MessageBus;

namespace Cashier.Service
{
    public static class ServiceLocator
    {
        public static IMessageBus Bus { get; set; }
        public static OrderCommandHandlers BrandCommands { get; set; }
    }
}