using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class ChangeCostItemResponse
    {
        public Guid Id { get; set; }
        public string NewDescription { get; set; }
        public int OriginalVersion { get; set; }
    }
}
