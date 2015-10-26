using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.Helpers.Exceptions
{
    public class CommandVersionException : Exception
    {
        public readonly Guid Id;
        public readonly Type Type;
        public readonly int Version;

        public CommandVersionException(Guid id, Type type, int version)
            : base(string.Format("Command '{0}' (type {1}) could not be applied. Incorrect Aggregate Version {2}.", id, type.Name, version))
        {
            Id = id;
            Type = type;
            Version = version;
        }
    }
}