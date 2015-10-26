using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.MasterData
{
    /// <summary>
    /// WebGateway Response to ArchiveOperatorCommand: PUT api/operator/{id}/archive
    /// </summary>
    public class ArchiveOperatorResponse
    {
        public Guid Id { get; set; }
        public bool NewArchive { get; set; }
        public int OriginalVersion { get; set; }
    }
}