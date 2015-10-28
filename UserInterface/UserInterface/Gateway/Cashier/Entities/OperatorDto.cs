using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.MasterData
{
    public class OperatorDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; }
        public int Version { get; set; }
    }
}