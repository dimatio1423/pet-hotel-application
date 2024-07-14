﻿using BusinessObjects.Entities;
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

        [Range(50000.00, 300000.00, ErrorMessage = "Price must be between 50,000 and 300,000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="Status is required")]
        public string Status { get; set; }
    }
}
