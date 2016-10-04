using MicroServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barista.Service.CachedData
{
    public class Product : ReadObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
