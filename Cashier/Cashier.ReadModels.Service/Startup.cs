using System;
using System.Net;
using System.Web.Http;
using Owin;
using Cashier.ReadModels.Service.Views;
using MicroServices.Common.Exceptions;
using MicroServices.Common.MessageBus;
using MicroServices.Common.Repository;
using EasyNetQ;
using Newtonsoft.Json;
using Cashier.Common.Dto;
using MicroServices.Common;
using MicroServices.Common.General.Util;
using StackExchange.Redis;
using Aggregate = MicroServices.Common.Aggregate;

namespace Cashier.ReadModels.Service
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
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            return config;
        }

        private void ConfigureHandlers()
        {
            // ------------------------------------------------------------------------------------
            // SJC : this is using the app.config to indicate settings
            //       BUT
            //       this maybe a problem for the octopus deploy mechanism.
            // ------------------------------------------------------------------------------------
            //var appConfig = AppConfiguration.Config;
            var redis = ConnectionMultiplexer.Connect("localhost");
            var brandView = new OrderView(new RedisReadModelRepository<OrderDto>(redis.GetDatabase()));
            ServiceLocator.BrandView = brandView;

            var eventMappings = new EventHandlerDiscovery()
                            .Scan(brandView)
                            .Handlers;

            var messageBusEndPoint = "masterdata_readmodel";
            var topicFilter = "Cashier.Common.Events";

            var b = RabbitHutch.CreateBus("host=localhost");

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

            var bus = new RabbitMqBus(b);

            ServiceLocator.Bus = bus;
        }
    }
}