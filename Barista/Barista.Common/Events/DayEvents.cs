using System;
using Newtonsoft.Json;
using Barista.Common.Enums;
using MicroServices.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Barista.Common.Events
{
    public class DayCreated : Event
    {
        public string Description { get; private set; }

        public DayCreated(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
        [JsonConstructor]
        private DayCreated(Guid id, string description, int version) : this (id, description)
        {
            Version = version;
        }
    }

    public class DayDescriptionChanged : Event
    {
        public DayDescriptionChanged(Guid id, string newDescription)
        {
            Id = id;
            NewDescription = newDescription;
        }

        [JsonConstructor]
        private DayDescriptionChanged(Guid id, string newDescription, int version) : this(id, newDescription)
        {
            Version = version;
        }

        public string NewDescription { get; private set; }
    }

    public class DayDeleted : Event
    {
        public DayDeleted(Guid id)
        {
            Id = id;
        }

        [JsonConstructor]
        private DayDeleted(Guid id, int version)
            : this(id)
        {
            Version = version;
        }
    }



    public class DayCostItemAdded : Event
    {
        public DayCostItemAdded(Guid id, Guid costitemId)
        {
            Id = id;
            CostItemId = costitemId;
        }

        [JsonConstructor]
        private DayCostItemAdded(Guid id, Guid costitemId, int version)
            : this(id, costitemId)
        {
            Version = version;
        }

        public Guid CostItemId { get; private set; }
    }

    public class DayCostItemRemoved : Event
    {
        public DayCostItemRemoved(Guid id, Guid costitemId)
        {
            Id = id;
            CostItemId = costitemId;
        }

        [JsonConstructor]
        private DayCostItemRemoved(Guid id, Guid costitemId, int version)
            : this(id, costitemId)
        {
            Version = version;
        }

        public Guid CostItemId { get; private set; }
    }


    public class DayCostItemsReordered : Event
    {
        public DayCostItemsReordered(Guid id, Collection<Guid> costitems)
        {
            Id = id;
            CostItems = costitems;
        }

        [JsonConstructor]
        private DayCostItemsReordered(Guid id, Collection<Guid> costitems, int version)
            : this(id, costitems)
        {
            Version = version;
        }

        public Collection<Guid> CostItems { get; private set; }
    }
}
