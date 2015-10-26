using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Data;

namespace UserInterface.Models.Entities
{
    public class ItineraryDto
    {
        public Guid Id { get; set; }
        public int Version { get; set; }

        public string Name { get; set; }
        public Guid SeasonId { get; set; }
        public Guid ProductId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid BrandId { get; set; }
        public Guid OperatorId { get; set; }
        public Guid RegionId { get; set; }
        public bool IsDeleted { get; set; }
        public int Status { get; set; } // This is actually an enumeration value.
        public List<Guid> ItineraryDayIds { get; set; }

        public string DisplayName { get; set; }
        public string ItineraryStatusDisplayName { get; set; }

        // Association
        public BrandDto Brand { get; set; }
        public OperatorDto Operator { get; set; }
        public SeasonDto Season { get; set; }
        public ProductDto Product { get; set; }
        public List<DayDto> ItineraryDays { get; set; }
    }
}
