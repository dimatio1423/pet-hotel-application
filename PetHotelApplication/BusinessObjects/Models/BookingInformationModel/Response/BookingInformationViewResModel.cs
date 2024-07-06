using BusinessObjects.Entities;
using BusinessObjects.Models.PetCareModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.BookingInformationModel.Response
{
    public class BookingInformationViewResModel
    {
        public string BoardingType { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string Note { get; set; }

        public string Accommodation { get; set; }

        public string Pet { get; set; }
    }
}
