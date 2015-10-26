using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroServices.Common;

namespace Barista.Common.Dto
{
    public class DayDto : ReadObject
    {
        public string Description { get; set; }
        public int Version { get; set; }
        public bool IsDeleted { get; set; }

        public Collection<Guid> CostItems { get; set; }
    }
}
