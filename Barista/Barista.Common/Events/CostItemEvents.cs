using System;
using Newtonsoft.Json;
using Barista.Common.Enums;
using MicroServices.Common;

namespace Barista.Common.Events
{
    public class CostItemCreated : Event
    {
        public string Description { get; private set; }

        public CostItemCreated(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
        [JsonConstructor]
        private CostItemCreated(Guid id, string description, int version) : this (id, description)
        {
            Version = version;
        }
    }

    public class CostItemDescriptionChanged : Event
    {
        public CostItemDescriptionChanged(Guid id, string newDescription)
        {
            Id = id;
            NewDescription = newDescription;
        }

        [JsonConstructor]
        private CostItemDescriptionChanged(Guid id, string newDescription, int version) : this(id, newDescription)
        {
            Version = version;
        }

        public string NewDescription { get; private set; }
    }

    public class CostItemDeleted : Event
    {
        public CostItemDeleted(Guid id)
        {
            Id = id;
        }

        [JsonConstructor]
        private CostItemDeleted(Guid id, int version)
            : this(id)
        {
            Version = version;
        }
    }

}
