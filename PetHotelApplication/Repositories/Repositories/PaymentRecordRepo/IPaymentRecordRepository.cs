using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.PaymentRecordRepo
{
    public interface IPaymentRecordRepository
    {
        List<PaymentRecord> GetPaymentRecords();
        PaymentRecord GetPaymentRecordById(string id);
        void Add(PaymentRecord paymentRecord);
        void Delete(PaymentRecord paymentRecord);
        void Update(PaymentRecord paymentRecord);
        List<PaymentRecord> GetPaymentRecordsFromBookingId(string bookingId);
        void UpdateRange(List<PaymentRecord> paymentRecords);
    }
}
