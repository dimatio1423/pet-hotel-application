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
        [Range(1, 20, ErrorMessage = "Please input a number between 1 and 20")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Please input accommodation description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please input accommodation price")]
        [Range(50000.00, 200000.00, ErrorMessage = "Price must be between 50,000 and 200,000")]
        public decimal Price { get; set; }
    }

    public class AccommodationTypeEnumModel
    {
        public int EnumId { get; set; }
        public string? EnumValue { get; set; }
    }
}
