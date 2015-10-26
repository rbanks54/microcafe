using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.MasterData
{
    public class BrandDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}