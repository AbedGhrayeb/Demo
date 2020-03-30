using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ViewModels
{
    public class NewPetVM
    {
        [Required]
        [MinLength(3, ErrorMessage ="{0} must be more than or equal {1} characters" )]
        [Remote(action:"VerifyName",controller:"Pets")]
        public string Name { get; set; }
        [Required]
        [Range(1,5,ErrorMessage ="{0} must be between {1} and {2}")]
        public int Age { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
