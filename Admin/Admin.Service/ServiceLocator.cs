using Admin.Service.MicroServices.Products.Handlers;
using MicroServices.Common.MessageBus;

namespace Admin.Service
{
    public static class ServiceLocator
    {
        public static IMessageBus Bus { get; set; }
        public static ProductCommandHandlers ProductCommands { get; set; }
    }
}