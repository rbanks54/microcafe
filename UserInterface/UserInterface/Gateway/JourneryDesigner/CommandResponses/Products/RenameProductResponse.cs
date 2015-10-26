using System;

namespace UserInterface.Models.CommandResponses
{
    /// <summary>
    /// WebGateway Response to RenameProductCommand: PUT api/products
    /// </summary>
    public class AlterProductResponse
    {
        public Guid Id { get; set; }
        public string NewCode { get; set; }
        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public int OriginalVersion { get; set; }
    }
}