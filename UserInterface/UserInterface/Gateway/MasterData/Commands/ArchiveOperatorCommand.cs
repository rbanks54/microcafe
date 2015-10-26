using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UserInterface.MasterData
{
    /// <summary>
    /// ApiGateway Command Parameters for PUT api/operators/{id}/archive
    /// </summary>
    public class ArchiveOperatorCommand
    {
        public bool Archive { get; set; }

        public int Version { get; set; }
    }
}