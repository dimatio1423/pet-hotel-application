using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.PaymentRecordModel.Response
{
    public class PaymentRecordResModel
    {
        public string Id { get; set; }

        public string Price { get; set; }

        public DateTime Date { get; set; }

        public string Method { get; set; }

        public string Status { get; set; }
    }
}
