using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Common.MessageBus
{
    public class TransientSubscriber : IDisposable
    {
        private IBus bus;
        private ISubscriptionResult subscription;

        private Action handler;
        private string topic;
        private string listenerName;

        public TransientSubscriber(string listenerName, string topic, Action handler)
        {
            this.listenerName = listenerName;
            this.topic = topic;
            this.handler = handler;

            InitialiseBus();
        }

        private void InitialiseBus()
        {
            bus = RabbitHutch.CreateBus("host=localhost");
            subscription = bus.Subscribe<PublishedMessage>(listenerName, m => handler(), q => q.WithTopic(topic));
        }

        public void Dispose()
        {
            subscription.Dispose();
        }
    }
}
