using Barista.Common.Enums;

namespace Barista.Service.DataTransferObjects.Commands
{
    /// <summary>
    /// WebApi Command Parameters for api/products/createproduct
    /// </summary>
    public class CreateProductCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
    }
}
