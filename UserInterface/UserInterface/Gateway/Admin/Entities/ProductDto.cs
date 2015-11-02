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
        public decimal Price { get; set; }
        public int Version { get; set; }

        public string DisplayName { get; set; }
    }
}
