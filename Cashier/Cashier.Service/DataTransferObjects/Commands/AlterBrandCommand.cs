namespace Cashier.Service.DataTransferObjects.Commands
{
    /// <summary>
    ///     WebApi Command Parameters for api/brands/alterbrand
    /// </summary>
    public class AlterBrandCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}