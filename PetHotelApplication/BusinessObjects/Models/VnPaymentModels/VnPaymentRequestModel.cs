using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.VnPaymentModels
{
    public class VnPaymentRequestModel
    {
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
