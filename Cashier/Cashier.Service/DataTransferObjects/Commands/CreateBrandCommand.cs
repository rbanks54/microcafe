namespace Cashier.Service.DataTransferObjects.Commands
{
    /// <summary>
    ///     WebApi Command Parameters for api/brands/createbrand
    /// </summary>
    public class CreateBrandCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}