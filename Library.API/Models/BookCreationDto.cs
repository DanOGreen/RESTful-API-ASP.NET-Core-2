using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library.API.Models
{
    public class BookCreationDto : BookForManipulationDto
    {
        [Required(ErrorMessage = "You should fill out a description")]
        public override string Description { get; set; }
    }
}
