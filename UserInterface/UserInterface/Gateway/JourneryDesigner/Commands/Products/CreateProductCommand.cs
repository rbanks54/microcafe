using System.ComponentModel.DataAnnotations;
using UserInterface.Models.Entities;

namespace UserInterface.Models.JourneryDesigner.Commands
{
    /// <summary>
    /// ApiGateway Command Parameters for POST api/products
    /// </summary>
    public class CreateProductCommand
    {
        [MaxLength(4, ErrorMessage = "Code must be less than or equal to 4 characters")]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }

    }
}