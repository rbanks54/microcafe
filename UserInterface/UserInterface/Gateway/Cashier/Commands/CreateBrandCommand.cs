using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.MasterData
{
    /// <summary>
    /// ApiGateway Command Parameters for POST api/masterdata/
    /// </summary>
    public class CreateBrandCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}