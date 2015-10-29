namespace Admin.Service.DataTransferObjects.Commands
{
    /// <summary>
    /// WebApi Command Parameters for api/products/createproduct
    /// </summary>
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
