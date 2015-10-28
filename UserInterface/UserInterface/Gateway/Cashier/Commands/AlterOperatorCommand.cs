using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UserInterface.MasterData
{
    /// <summary>
    /// ApiGateway Command Parameters for PUT api/operators/{id}
    /// </summary>
    public class AlterOperatorCommand
    {
        [Required]
        [RegularExpression("^[A-Z]+$", ErrorMessage = "Code accepts uppercase letters only. Please remove any numbers or punctuation.")]
        [StringLength(4)]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        public int Version { get; set; }
    }
}