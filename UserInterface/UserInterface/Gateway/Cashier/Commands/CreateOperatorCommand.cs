using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UserInterface.MasterData
{
    /// <summary>
    /// ApiGateway Command Parameters for POST api/operators
    /// </summary>
    public class CreateOperatorCommand
    {
        [Required]
        [RegularExpression("^[A-Z]+$", ErrorMessage= "Code accepts uppercase letters only. Please remove any numbers or punctuation.")]
        [StringLength(4)]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }
    }
}