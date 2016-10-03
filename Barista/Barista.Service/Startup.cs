using System.Net;
using System.Web.Http;
using EventStore.ClientAPI;
using Owin;
using Barista.Service.MicroServices.Orders.Handlers;
using MicroServices.Common.MessageBus;
using MicroServices.Common.Repository;
using EasyNetQ;
using Admin.ReadModels.Client;
using MicroServices.Common.General.Util;
using MicroServices.Common;
using System;
using Newtonsoft.Json;

namespace Barista.Service
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = ConfigureWebApi();
            app.UseWebApi(webApiConfiguration);
            LoadExternalData();
            ConfigureHandlers();
        }

        private void LoadExternalData()
        {
            IProductsView masterDataClient = new ProductsView();
            masterDataClient.Initialise();
        }

        private static HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            return config;
        }

        private void ConfigureHandlers()
        {
            var b = RabbitHutch.CreateBus("host=localhost");

            var messageBusEndPoint = "barista";
            var topicFilter = "Cashier.Common.Events";

            var bus = new RabbitMqBus(b);
            ServiceLocator.Bus = bus;

            //Should get this from a config setting instead of hardcoding it.
            var eventStoreConnection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 12900));
            eventStoreConnection.ConnectAsync().Wait();
            var repository = new EventStoreRepository(eventStoreConnection, bus);
            
            ServiceLocator.OrderCommands = new OrderCommandHandlers(repository);

            var cashierEventHandler = new CashierOrderEventHandler(repository);
            ServiceLocator.CahierEventHandler = cashierEventHandler;

            var eventMappings = new EventHandlerDiscovery()
                .Scan(cashierEventHandler)
                .Handlers;

            b.Subscribe<PublishedMessage>(messageBusEndPoint,
            m =>
            {
                Aggregate handler;
                var messageType = Type.GetType(m.MessageTypeName);
                var handlerFound = eventMappings.TryGetValue(messageType, out handler);
                if (handlerFound)
                {
                    var @event = JsonConvert.DeserializeObject(m.SerialisedMessage, messageType);
                    handler.AsDynamic().ApplyEvent(@event, ((Event)@event).Version);
                }
            },
            q => q.WithTopic(topicFilter));

        }
    }
}