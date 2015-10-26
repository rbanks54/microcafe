using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.MasterData
{
    /// <summary>
    /// ApiGateway Command Parameters for PUT api/masterdata/brands/{id}
    /// </summary>
    public class AlterBrandCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}