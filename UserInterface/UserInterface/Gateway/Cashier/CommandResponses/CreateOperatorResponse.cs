using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace UserInterface.MasterData
{
    /// <summary>
    /// WebGateway Response to CreateOperatorCommand: POST api/operators
    /// </summary>
    public class CreateOperatorResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}