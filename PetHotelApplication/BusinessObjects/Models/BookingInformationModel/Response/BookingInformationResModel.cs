using BusinessObjects.Models.PetModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.BookingInformationModel.Response
{
    public class BookingInformationResModel
    {
        public string Id { get; set; }

        public string BoardingType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Note { get; set; }

        public string Status { get; set; }

        public AccommodationResModel Accommodation { get; set; }

        public string PetCareServices { get; set; }

        public PetResModel Pet {  get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }

    public class AccommodationResModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
