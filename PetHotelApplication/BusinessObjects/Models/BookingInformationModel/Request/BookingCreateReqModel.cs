using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.BookingInformationModel.Request
{
    public class BookingCreateReqModel
    {
        [Required(ErrorMessage = "Please select a boarding type")]
        public string BoardingType { get; set; }

        [Required(ErrorMessage = "Please provide a start date")]
        [DataType(DataType.Date, ErrorMessage = "Please provide a valid start date")]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = "Please provide an end date")]
        [DataType(DataType.Date, ErrorMessage = "Please provide a valid end date")]
        public DateOnly EndDate { get; set; }

        //[StringLength(500, ErrorMessage = "Note cannot be longer than 500 characters")]
        public string? Note { get; set; } = "";

        [Required(ErrorMessage = "Please select an accommodation")]
        public string AccommodationId { get; set; }

        [Required(ErrorMessage = "Please select a pet")]
        public string PetId { get; set; }
    }
}
