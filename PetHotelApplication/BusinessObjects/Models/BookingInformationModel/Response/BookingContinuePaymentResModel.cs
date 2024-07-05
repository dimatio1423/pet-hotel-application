using BusinessObjects.Entities;
using BusinessObjects.Models.BookingInformationModel.Response;
using BusinessObjects.Models.PetCareModel.Response;
using BusinessObjects.Models.PetModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.BookingInformationModel.Response
{
    public class BookingContinuePaymentResModel
    {
        public string Id { get; set; }

        public string BoardingType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Note { get; set; }

        public string Status { get; set; }

        public string Accommodation { get; set; }

        public List<PetCareResModel> PetCareServices { get; set; }

        public string Pet { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
