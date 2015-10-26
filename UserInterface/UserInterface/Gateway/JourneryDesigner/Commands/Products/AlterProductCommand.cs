using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UserInterface.Models.Entities;

namespace UserInterface.Commands
{
    /// <summary>
    /// ApiGateway Command Parameters for PUT api/products/{id}
    /// </summary>
    public class AlterProductCommand
    {
        [MaxLength(4, ErrorMessage = "Code must be less than or equal to 4 characters")]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
        public int Version { get; set; }
    }
}