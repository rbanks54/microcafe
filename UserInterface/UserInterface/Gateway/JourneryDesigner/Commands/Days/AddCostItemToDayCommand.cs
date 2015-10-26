using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models.Commands
{   
    public class AddCostItemToDayCommand
    {
        public int Version { get; set; }
        public string Description { get; set; }
    }
}
