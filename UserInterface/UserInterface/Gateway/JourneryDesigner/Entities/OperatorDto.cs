using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.Models.Entities
{
    public class OperatorDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; }

        public string DisplayName { get { return string.Format("{0} ({1})", Description, Code); } }
    }
}