using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.Gateway.JourneryDesigner.Commands.Itinerary
{
    public class ReorderDaysForItineraryCommand
    {
        public int Version { get; set; }
        public List<Guid> NewOrder { get; set; }
    }
}