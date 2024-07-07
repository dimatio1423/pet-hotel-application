using BusinessObjects.Entities;
using BusinessObjects.Enums.StatusEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.PetCareServiceModel.Request
{
    public class PetCareServiceUpdateReqModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="Status is required")]
        public string Status { get; set; }
    }
}
