using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class ReorderCostItemsForDayResponse
    {
        public Guid Id { get; set; }
        public Collection<Guid> CostItems { get; set; }
        public int Version { get; set; }
    }
}
