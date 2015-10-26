using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.Helpers.Exceptions
{
    public class DtoNotFoundException : Exception
    {
        public readonly Guid Id;
        public readonly Type Type;

        public DtoNotFoundException(Guid id, Type type)
            : base(string.Format("DTO '{0}' (type {1}) was not found.", id, type.Name))
        {
            Id = id;
            Type = type;
        }
    }
}