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

        [Required(ErrorMessage = "Pet age is required")]
        [Range(1, 50, ErrorMessage = "Pet age must be in range {1} and {2}")]
        public int Age { get; set; }
    }

    public class BookingInformationResModel
    {
        public string Id { get; set; }

        public string BoardingType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Note { get; set; }

        public string Status { get; set; }

        public AccommodationResModel Accommodation { get; set; }

        public ICollection<PaymentRecordResModel> PaymentRecords { get; set; } = new List<PaymentRecordResModel>();

        public string PetCareServices { get; set; }
    }

    public class AccommodationResModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }

    public class PaymentRecordResModel
    {
        public string Id { get; set; }

        public string Price { get; set; }

        public DateTime Date { get; set; }

        public string Method { get; set; }

        public string Status { get; set; }
    }
}
