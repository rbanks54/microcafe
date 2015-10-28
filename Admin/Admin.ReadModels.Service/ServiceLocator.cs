using Admin.ReadModels.Service.Views;
using MicroServices.Common.MessageBus;

namespace Admin.ReadModels.Service
{
    public static class ServiceLocator
    {
        public static IMessageBus Bus { get; set; }
        public static ProductView ProductView { get; set; }
    }
}