using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.Models.Entities
{
    public class ItinerariesRefData
    {
        public IQueryable<SeasonDto> SeasonDtos { get; set; }
    }
}