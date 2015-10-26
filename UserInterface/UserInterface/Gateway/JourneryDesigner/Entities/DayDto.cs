using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Data
{
    public class DayDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }

        public Collection<CostItemDto> CostItems { get; set; }
    }
}
