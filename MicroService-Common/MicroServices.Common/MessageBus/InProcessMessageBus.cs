using System;
using System.Collections.Generic;
using System.Threading;

namespace MicroServices.Common.MessageBus
{
    public class InProcessMessageBus : IMessageBus
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> commandHandlers = new Dictionary<Type, List<Action<IMessage>>>();

        //Registers handlers for both events and commands
        public void RegisterHandler<T>(Action<T> handler) where T : IMessage
        {
            List<Action<IMessage>> handlers;

            if (!commandHandlers.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<IMessage>>();
                commandHandlers.Add(typeof(T), handlers);
            }

            handlers.Add((msg => handler((T)msg)));
        }

        public void Publish<T>(T @event) where T : Event
        {
            List<Action<IMessage>> handlers;

            if (!commandHandlers.TryGetValue(@event.GetType(), out handlers)) return;

            foreach (var handler in handlers)
            {
                //dispatch on thread pool for added awesomeness
                var handler1 = handler;
                ThreadPool.QueueUserWorkItem(x => handler1(@event));
            }
        }

        //the inproc message bus only sends command messages to one handler
        public void Send<T>(T command) where T : class,ICommand
        {
            List<Action<IMessage>> matchingHandlers;

            if (commandHandlers.TryGetValue(typeof(T), out matchingHandlers))
            {
                if (matchingHandlers.Count != 1) throw new InvalidOperationException("cannot send commands to more than one handler");
                matchingHandlers[0](command);
            }
            else
            {
                throw new InvalidOperationException("no command handler registered");
            }
        }
    }
}
