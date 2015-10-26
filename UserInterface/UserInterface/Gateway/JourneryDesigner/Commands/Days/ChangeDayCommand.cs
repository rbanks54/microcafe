using System;
using System.ComponentModel.DataAnnotations;

namespace UserInterface.Models.Commands
{
    public class ChangeDayCommand
    {
        public string Description { get; set; }
        public int Version { get; set; }
    }
}