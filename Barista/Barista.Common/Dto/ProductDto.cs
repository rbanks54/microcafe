using System;
using Barista.Common.Enums;
using MicroServices.Common;

namespace Barista.Common.Dto
{
    public class ProductDto : ReadObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
        public ProductType ProductType { get; set; }

        public string DisplayName { get; set; }
        public string ProductTypeDisplayName { get; set; }
    }
}

