using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UserInterface.Models.Entities;

namespace UserInterface.Gateway.Admin.Commands.Products
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
    }

    public class AlterProductCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
        public int Version { get; set; }
    }
}