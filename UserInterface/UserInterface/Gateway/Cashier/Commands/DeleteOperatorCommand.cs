using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UserInterface.MasterData
{
    /// <summary>
    /// ApiGateway Command Parameters for DELETE api/operators/{id}
    /// </summary>
    public class DeleteOperatorCommand
    {
        public int Version { get; set; }
    }
}