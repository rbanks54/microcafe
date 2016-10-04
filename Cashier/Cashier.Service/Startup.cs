using System.Net;
using System.Web.Http;
using EventStore.ClientAPI;
using Owin;
using Cashier.Service.Config;
using Cashier.Service.MicroServices.Order.Handlers;
using MicroServices.Common.MessageBus;
using MicroServices.Common.Repository;
using EasyNetQ;
using MicroServices.Common.General.Util;
using Cashier.Service.MicroServices.Product.Handlers;
using MicroServices.Common;
using System;
using Newtonsoft.Json;
using Cashier.Service.MicroServices.Product.View;

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
            var b = RabbitHutch.CreateBus("host=localhost");
            var bus = new RabbitMqBus(b);
            ServiceLocator.Bus = bus;

            var messageBusEndPoint = "cashier_service";
            var topicFilter = "Admin.Common.Events";

            var eventStorePort = 12900;

            var eventStoreConnection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, eventStorePort));
            eventStoreConnection.ConnectAsync().Wait();
            var repository = new EventStoreRepository(eventStoreConnection, bus);

            ServiceLocator.OrderCommands = new OrderCommandHandlers(repository);
            ServiceLocator.ProductView = new ProductView();

            var eventMappings = new EventHandlerDiscovery()
                .Scan(new AdminEventsHandler())
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