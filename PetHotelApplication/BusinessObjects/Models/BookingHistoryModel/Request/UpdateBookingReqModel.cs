using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.BookingHistoryModel.Request
{
    public class UpdateBookingReqModel
    {
        public string Id { get; set; }
        public string Note { get; set; }
        public string AccommodationType { get; set; }
        public List<string> ServiceTypes { get; set; }
    }
}
