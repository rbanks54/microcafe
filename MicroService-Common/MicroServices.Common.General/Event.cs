using System;

namespace MicroServices.Common
{
    public interface IEvent : IMessage
    {
        Guid Id { get; }
        int Version { get; }
    }

    public class Event : IEvent
    {
        public Guid Id { get; protected set; }
        public int Version { get; internal protected set; }
    }
}