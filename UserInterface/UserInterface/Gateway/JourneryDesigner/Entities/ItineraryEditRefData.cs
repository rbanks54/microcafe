using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.Models.Entities
{
    public class ItineraryEditRefData
    {
        public IQueryable<OwnerDto> OwnerDtos { get; set; }
        public IQueryable<BrandDto> BrandDtos { get; set; }
        public IQueryable<OperatorDto> OperatorDtos { get; set; }
        public IQueryable<SeasonDto> SeasonDtos { get; set; }
        public IQueryable<ProductDto> ProductDtos { get; set; }
    }
}