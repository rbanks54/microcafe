using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models.Commands
{   
    public class RemoveCostItemFromDayCommand
    {
        public Guid CostItemId { get; set; }
        public int Version { get; set; }
    }
}
