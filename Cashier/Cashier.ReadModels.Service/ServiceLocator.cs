using Cashier.ReadModels.Service.Views;
using MicroServices.Common.MessageBus;
using StackExchange.Redis;

namespace Cashier.ReadModels.Service
{
    public static class ServiceLocator
    {
        public static IMessageBus Bus { get; set; }
        public static BrandView BrandView { get; set; }
    }
}