using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc.Html;

namespace UserInterface.Models.Entities
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
        public int Version { get; set; }

        public string DisplayName { get; set; }
        public string ProductTypeDisplayName { get; set; }
    }

    public enum ProductType
    {
        [Display(Name = "River Cruise")]
        RiverCruise = 1,
        [Display(Name = "Land Tour")]
        LandTour = 2,
        [Display(Name = "Ocean Cruise")]
        OceanCruise
    }
}
