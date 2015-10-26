using System;
using System.CodeDom;
using Newtonsoft.Json;
using MicroServices.Common;

namespace Cashier.Common.Events
{
    public class BrandCreated : Event
    {
        public BrandCreated(Guid id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        [JsonConstructor]
        private BrandCreated(Guid id, string code, string name, int version) : this(id, code, name)
        {
            Version = version;
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
    }

    public class BrandCodeChanged : Event
    {
        public BrandCodeChanged(Guid id, string newCode)
        {
            Id = id;
            NewCode = newCode;
        }

        [JsonConstructor]
        private BrandCodeChanged(Guid id, string newCode, int version) : this(id, newCode)
        {
            Version = version;
        }

        public string NewCode { get; private set; }
    }

    public class BrandNameChanged : Event
    {
        public BrandNameChanged(Guid id, string newName)
        {
            Id = id;
            NewName = newName;
        }

        [JsonConstructor]
        private BrandNameChanged(Guid id, string newName, int version) : this(id, newName)
        {
            Version = version;
        }

        public string NewName { get; private set; }
    }

    public class BrandPublished : Event
    {
        public BrandPublished(Guid id, string code, string title, string description)
        {
            Id = id;
            Code = code;
            Title = title;
            Description = description;
        }

        [JsonConstructor]
        private BrandPublished(Guid id, string code, string title, string description, int version) : this(id, code, title, description)
        {
            Version = version;
        }
        
        public string Code { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
    }
}