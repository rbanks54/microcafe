using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class RemoveCostItemFromDayResponse
    {
        public Guid Id { get; set; }
        public Guid CostItemId { get; set; }
        public int Version { get; set; }
    }
}
