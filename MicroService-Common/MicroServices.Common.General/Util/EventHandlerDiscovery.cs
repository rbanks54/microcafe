using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Common.General.Util
{
    public class EventHandlerDiscovery
    {
        public Dictionary<Type, Aggregate> Handlers
        {
            get; private set;
        }

        public EventHandlerDiscovery()
        {
            Handlers = new Dictionary<Type, Aggregate>();
        }

        public EventHandlerDiscovery Scan(Aggregate aggregate)
        {
            var handlerInterface = typeof(IHandle<>);
            var aggType = aggregate.GetType();

            var interfaces = aggType.GetInterfaces();

            var instances = from i in aggType.GetInterfaces()
                            where (i.IsGenericType && handlerInterface.IsAssignableFrom(i.GetGenericTypeDefinition()))
                            select i.GenericTypeArguments[0];

            foreach (var i in instances)
            {
                Handlers.Add(i, aggregate);
            }

            return this;
        }
    }
}
