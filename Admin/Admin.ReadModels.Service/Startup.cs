using System.Net;
using System.Web.Http;
using Owin;
using MicroServices.Common.MessageBus;
using MicroServices.Common.Repository;
using Admin.ReadModels.Service.Views;
using MicroServices.Common.Exceptions;
using EasyNetQ;
using MicroServices.Common;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Admin.Common.Dto;
using Admin.Common.Events;
using StackExchange.Redis;
using Aggregate = MicroServices.Common.Aggregate;
using MicroServices.Common.General.Util;

namespace Admin.ReadModels.Service
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = ConfigureWebApi();
            webApiConfiguration.EnsureInitialized();
            app.UseWebApi(webApiConfiguration);
            ConfigureHandlers();
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
            var redis = ConnectionMultiplexer.Connect("localhost");
            var productView = new ProductView(new RedisReadModelRepository<ProductDto>(redis.GetDatabase()));
            ServiceLocator.ProductView = productView;

            var eventMappings = new EventHandlerDiscovery()
                                .Scan(productView)
                                .Handlers;

            var subscriptionName = "admin_readmodel";
            var topicFilter1 = "Admin.Common.Events";

            var b = RabbitHutch.CreateBus("host=localhost");

            b.Subscribe<PublishedMessage>(subscriptionName,
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
            q => q.WithTopic(topicFilter1));

            var bus = new RabbitMqBus(b);

            ServiceLocator.Bus = bus;
        }
    }
}