using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.Models.Entities
{
    public class SeasonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }

        public string DisplayName { get { return string.Format("{0}", Name); } }
    }
}