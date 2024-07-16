using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.BookingInformationModel.Response
{
    public class BookingListInformationResModel
    {
        public string Id { get; set; } = null!;

        public string BoardingType { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PetName { get; set; } = null!;
    }
}
