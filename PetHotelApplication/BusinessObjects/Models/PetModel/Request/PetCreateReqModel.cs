using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.PetModel.Request
{
    public class PetCreateReqModel
    {
        [AllowNull]
        public string? Avatar { get; set; }

        [Required(ErrorMessage = "Pet name is required")]
        public string PetName { get; set; }

        [Required(ErrorMessage = "Pet species is required")]
        public string Species { get; set; }

        [Required(ErrorMessage = "Pet breed is required")]
        public string Breed { get; set; }

        [Required(ErrorMessage = "Pet age is required")]
        [Range(1, 50, ErrorMessage = "Pet age must be in range {1} and {2}")]
        public int Age { get; set; }
    }
}
