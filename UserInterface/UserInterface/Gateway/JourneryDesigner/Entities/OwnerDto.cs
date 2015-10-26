using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.Models.Entities
{
    public class OwnerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string DisplayName { get { return string.Format("{0}", Name); } }
    }
}