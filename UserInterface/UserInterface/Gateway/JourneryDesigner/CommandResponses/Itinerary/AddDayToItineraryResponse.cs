using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class AddDayToItineraryResponse
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public Guid DayId { get; set; }
    }
}
