using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using Newtonsoft.Json;

namespace MicroServices.Common.MessageBus
{
    public class RabbitMqBus : IMessageBus
    {
        private readonly IBus bus;

        public IBus Bus { get { return bus; } }

        public RabbitMqBus(IBus easyNetQBus)
        {
            if (easyNetQBus == null)
            {
                throw new ArgumentNullException("easyNetQBus");
            }
            bus = easyNetQBus;
        }

        public void Publish<T>(T @event) where T : Event
        {
            if (bus != null)
            {
                var innerMessage = JsonConvert.SerializeObject(@event);

                var eventType = @event.GetType();
                var typeName = eventType.ToString();
                var topicName = typeName.Substring(0, typeName.LastIndexOf("."));

                var message = new PublishedMessage() { MessageTypeName = eventType.AssemblyQualifiedName, SerialisedMessage = innerMessage };

                bus.PublishAsync(message, topicName).Wait();
            }
            else
            {
                throw new ApplicationException("RabbitMqBus is not yet initialised");
            }
        }

        void IMessageBus.Send<T>(T command)
        {
            throw new NotImplementedException();
        }
    }

    public class PublishedMessage : IMessage
    {
        public string MessageTypeName { get; set; }
        public string SerialisedMessage { get; set; }
    }

}
