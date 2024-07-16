using BusinessObjects.CustomValidators;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.PetModel.Response
{
    public class PetResModel
    {
        public string Id { get; set; }

        public string? Avatar { get; set; }

        [Required(ErrorMessage = "Pet name is required")]
        public string PetName { get; set; }

        [Required(ErrorMessage = "Pet species is required")]
        public string Species { get; set; }

        [Required(ErrorMessage = "Pet breed is required")]
        public string Breed { get; set; }

        [Required(ErrorMessage = "Pet date of birth is required")]
        [Display(Name = "Date of birth")]
        [DateLessThanCurrentDate(ErrorMessage = "Date of birth must be in the past")]
        public DateOnly Dob { get; set; }

        public string Status { get; set; }
    }    
}
