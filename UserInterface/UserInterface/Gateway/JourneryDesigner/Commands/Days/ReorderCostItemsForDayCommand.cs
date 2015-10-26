using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserInterface.Models.Commands
{
    public class ReorderCostItemsForDayCommand
    {
        [Required]
        public Collection<Guid> CostItems { get; set; }
        public int Version { get; set; }
    }
}