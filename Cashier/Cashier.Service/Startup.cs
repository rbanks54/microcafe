using System.Net;
using System.Web.Http;
using EventStore.ClientAPI;
using Owin;
using Cashier.Service.Config;
using Cashier.Service.MicroServices.Order.Handlers;
using MicroServices.Common.MessageBus;
using MicroServices.Common.Repository;
using EasyNetQ;

namespace Cashier.Service
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = ConfigureWebApi();
            app.UseWebApi(webApiConfiguration);
            ConfigureHandlers();
        }

        private static HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();

            // Enable Web API Attribute Routing
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional});
            return config;
        }

        private void ConfigureHandlers()
        {
            var bus = new RabbitMqBus(RabbitHutch.CreateBus("host=localhost"));
            ServiceLocator.Bus = bus;

            var eventStorePort = 12800;

            var eventStoreConnection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, eventStorePort));
            eventStoreConnection.ConnectAsync().Wait();
            var repository = new EventStoreRepository(eventStoreConnection, bus);

            ServiceLocator.OrderCommands = new OrderCommandHandlers(repository);
        }
    }
}