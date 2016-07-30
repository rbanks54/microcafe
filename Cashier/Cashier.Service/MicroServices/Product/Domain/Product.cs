using MicroServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier.Service.MicroServices.Product.Domain
{
    public class Product : ReadObject
    {
        public decimal Price { get; set; }
    }
}
