using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class ChangeItineraryResponse
    {
        public Guid Id { get; set; }
        public string NewName { get; set; }
        public Guid NewSeasonId { get; set; }
        public Guid NewTourcodeId { get; set; }
        public Guid NewOwnerId { get; set; }
        public Guid NewBrandId { get; set; }
        public Guid NewOperatorId { get; set; }
        public Guid NewProductId { get; set; }
        public int OriginalVersion { get; set; }
    }
}
