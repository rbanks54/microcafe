using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using MicroServices.Common;
using Barista.Common.Enums;

namespace Barista.Common.Events
{
    public class ItineraryCreated : Event
    {
        public string Name { get; private set; }
        public Guid SeasonId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid OwnerId { get; private set; }
        public Guid BrandId { get; private set; }
        public Guid OperatorId { get; private set; }
        public ItineraryStatus Status { get; private set; }

        public ItineraryCreated(Guid id, string name, Guid seasonId, Guid productid, Guid ownerid, Guid brandid, Guid operatorid, ItineraryStatus status)
        {
            Id = id;
            Name = name;
            SeasonId = seasonId;
            ProductId = productid;
            OwnerId = ownerid;
            BrandId = brandid;
            OperatorId = operatorid;
            Status = status;
        }

        public ItineraryCreated(ItineraryCreated evt)
            : this(evt.Id, evt.Name, evt.SeasonId, evt.ProductId, evt.OwnerId, evt.BrandId, evt.OperatorId, evt.Status)
        {
        }

        [JsonConstructor]
        private ItineraryCreated(Guid id, string name, Guid seasonId, Guid productid, Guid ownerid, Guid brandid, Guid operatorid, ItineraryStatus status, int version)
            :this(id,name,seasonId,productid,ownerid,brandid,operatorid,status)
        {
            Version = version;
        }
    }

    public class ItineraryNameChanged : Event
    {
        public string Name { get; private set; }

        public ItineraryNameChanged(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        [JsonConstructor]
        private ItineraryNameChanged(Guid id, string name, int version) : this(id, name)
        {
            Version = version;
        }
    }

    public class ItinerarySeasonChanged : Event
    {
        public Guid SeasonId { get; private set; }

        public ItinerarySeasonChanged(Guid id, Guid seasonId)
        {
            Id = id;
            SeasonId = seasonId;
        }

        [JsonConstructor]
        private ItinerarySeasonChanged(Guid id, Guid seasonId, int version) : this(id, seasonId)
        {
            Version = version;
        }
    }

    public class ItineraryProductChanged : Event
    {
        public Guid ProductId { get; private set; }

        public ItineraryProductChanged(Guid id, Guid productid)
        {
            Id = id;
            ProductId = productid;
        }

        [JsonConstructor]
        private ItineraryProductChanged(Guid id, Guid productId, int version) : this(id, productId)
        {
            Version = version;
        }
    }

    public class ItineraryOwnerChanged : Event
    {
        public Guid OwnerId { get; private set; }

        public ItineraryOwnerChanged(Guid id, Guid ownerid)
        {
            Id = id;
            OwnerId = ownerid;
        }
        [JsonConstructor]
        private ItineraryOwnerChanged(Guid id, Guid ownerId, int version) : this(id, ownerId)
        {
            Version = version;
        }

    }

    public class ItineraryBrandChanged : Event
    {
        public Guid BrandId { get; private set; }

        public ItineraryBrandChanged(Guid id, Guid brandid)
        {
            Id = id;
            BrandId = brandid;
        }

        [JsonConstructor]
        private ItineraryBrandChanged(Guid id, Guid brandId, int version) : this(id, brandId)
        {
            Version = version;
        }
    }

    public class ItineraryOperatorChanged : Event
    {
        public Guid OperatorId { get; private set; }

        public ItineraryOperatorChanged(Guid id, Guid operatorid)
        {
            Id = id;
            OperatorId = operatorid;
        }

        [JsonConstructor]
        private ItineraryOperatorChanged(Guid id, Guid operatorId, int version) : this(id, operatorId)
        {
            Version = version;
        }
    }

    public class ItineraryStatusChanged : Event
    {
        public ItineraryStatus Status { get; private set; }

        public ItineraryStatusChanged(Guid id, ItineraryStatus status)
        {
            Id = id;
            Status = status;
        }

        [JsonConstructor]
        private ItineraryStatusChanged(Guid id, ItineraryStatus status, int version) : this(id, status)
        {
            Version = version;
        }
    }

    public class ItineraryDeleted : Event
    {
        public ItineraryDeleted(Guid id)
        {
            Id = id;
        }

        [JsonConstructor]
        private ItineraryDeleted(Guid id, int version) : this(id)
        {
            Version = version;
        }
    }

    public class ItineraryDayAdded : Event
    {
        public ItineraryDayAdded(Guid id, Guid dayId)
        {
            Id = id;
            DayId = dayId;
        }

        [JsonConstructor]
        private ItineraryDayAdded(Guid id, Guid dayId, int version) : this(id, dayId)
        {
            Version = version;
        }
        
        public Guid DayId{ get; private set; }
    }

    public class ItineraryDaysReordered : Event
    {
        public ItineraryDaysReordered(Guid id, Collection<Guid> itineraryDayIds)
        {
            Id = id;
            ItineraryDayIds = itineraryDayIds;
        }

        [JsonConstructor]
        private ItineraryDaysReordered(Guid id, Collection<Guid> itineraryDayIds, int version) : this(id, itineraryDayIds)
        {
            Version = version;
        }
        
        public Collection<Guid> ItineraryDayIds { get; private set; }
    }

    public class ItineraryDaysAltered : Event
    {
        public ItineraryDaysAltered(Guid id, Collection<Guid> itineraryDayIds)
        {
            Id = id;
            ItineraryDayIds = itineraryDayIds;
        }

        [JsonConstructor]
        private ItineraryDaysAltered(Guid id, Collection<Guid> itineraryDayIds, int version) : this(id, itineraryDayIds)
        {
            Version = version;
        }

        public Collection<Guid> ItineraryDayIds { get; private set; }
    }
}
