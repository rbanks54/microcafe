using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.MasterData
{
    /// <summary>
    /// WebGateway Response to RenameProductCommand: PUT api/masterdata/brands
    /// </summary>
    public class AlterBrandResponse
    {
        public Guid Id { get; set; }
        public string NewCode { get; set; }
        public string NewName { get; set; }
        public int OriginalVersion { get; set; }
    }
}