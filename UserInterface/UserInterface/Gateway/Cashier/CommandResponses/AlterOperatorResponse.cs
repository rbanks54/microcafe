using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.MasterData
{
    /// <summary>
    /// WebGateway Response to AlterOperatorCommand: PUT api/operators
    /// </summary>
    public class AlterOperatorResponse
    {
        public Guid Id { get; set; }
        public string NewCode { get; set; }
        public string NewDescription { get; set; }
        public int OriginalVersion { get; set; }
    }
}