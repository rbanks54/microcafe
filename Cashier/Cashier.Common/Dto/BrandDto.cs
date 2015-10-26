using MicroServices.Common;
using System;

namespace Cashier.Common.Dto
{
    public class BrandDto : ReadObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}