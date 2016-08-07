using System.Net;
using System.Web.Http;
using EventStore.ClientAPI;
using Owin;
using Barista.Service.MicroServices.Products.Handlers;
using MicroServices.Common.MessageBus;
using MicroServices.Common.Repository;
using EasyNetQ;
using Admin.ReadModels.Client;

namespace Barista.Service
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = ConfigureWebApi();
            app.UseWebApi(webApiConfiguration);
            ConfigureHandlers();
            LoadExternalData();
        }

        private void LoadExternalData()
        {
            IProductsView masterDataClient = new ProductsView();
            masterDataClient.Initialise();
        }

        private static HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();

            // Enable Web API Attribute Routing
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            return config;
        }

        private void ConfigureHandlers()
        {
            var bus = new RabbitMqBus(RabbitHutch.CreateBus("host=localhost"));
            ServiceLocator.Bus = bus;

            //Should get this from a config setting instead of hardcoding it.
            var eventStoreConnection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 12900));
            eventStoreConnection.ConnectAsync().Wait();
            var repository = new EventStoreRepository(eventStoreConnection, bus);
            
            ServiceLocator.ProductCommands = new ProductCommandHandlers(repository);
        }
    }
}