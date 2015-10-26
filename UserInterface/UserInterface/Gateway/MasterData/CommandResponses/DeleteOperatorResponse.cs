using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.MasterData
{
    /// <summary>
    /// WebGateway Response to DeleteOperatorCommand: DELETE api/operator
    /// </summary>
    public class DeleteOperatorResponse
    {
        public Guid Id { get; set; }
        public int OriginalVersion { get; set; }
    }
}