using Barista.ReadModels.Service.Views;
using MicroServices.Common.MessageBus;

namespace Barista.ReadModels.Service
{
    public static class ServiceLocator
    {
        public static IMessageBus Bus { get; set; }
        public static ProductView ProductView { get; set; }
    }
}