using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Barista.Common.Enums;
using MicroServices.Common;

namespace Barista.Common.Dto
{
    public class ItineraryDto : ReadObject
    {
        public int Version { get; set; }
        public string Name { get; set; }
        public Guid SeasonId { get; set; }
        public Guid ProductId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid BrandId { get; set; }
        public Guid OperatorId { get; set; }
        public Guid RegionId { get; set; }
        public ItineraryStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public Collection<Guid> ItineraryDayIds { get; set; }
        
        // Association
        public BrandDto Brand { get; set; }
        public OperatorDto Operator { get; set; }
        public SeasonDto Season { get; set; }
        public ProductDto Product { get; set; }
        public string DisplayName { get; set; }
        public string ItineraryStatusDisplayName { get; set; }
        public List<DayDto> ItineraryDays { get; set; }

    }
}
