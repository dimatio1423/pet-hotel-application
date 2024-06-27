using BusinessObjects.Enums.AccommodationTypeEnums;
using BusinessObjects.Enums.RoleEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.AccommodationModel.Request
{
    public class AccommodationCreateReqModel
    {

        [Required(ErrorMessage = "Please input accommodation name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please input accommodation type")]
        [EnumDataType(typeof(AccommodationTypeEnums), ErrorMessage = "Invalid type.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Please input accommodation capacity")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please input a number")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Please input accommodation description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please input accommodation price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }
    }

    public class AccommodationTypeEnumModel
    {
        public int EnumId { get; set; }
        public string? EnumValue { get; set; }
    }
}
