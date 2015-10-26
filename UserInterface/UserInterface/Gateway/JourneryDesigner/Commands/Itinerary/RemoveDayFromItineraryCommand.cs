using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models.Commands
{   
    public class RemoveDayFromItineraryCommand
    {
        public int Version { get; set; }
        public Guid RemovedDayId { get; set; }
        
    }
}
