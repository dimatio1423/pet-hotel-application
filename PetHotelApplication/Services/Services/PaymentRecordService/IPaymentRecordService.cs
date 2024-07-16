using BusinessObjects.Entities;
using BusinessObjects.Models.PaymentRecordModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.PaymentRecordService
{
    public interface IPaymentRecordService
    {
        List<PaymentRecord> GetPaymentRecords();
        PaymentRecord GetPaymentRecordById(string id);
        void Add(PaymentRecord paymentRecord);
        void Delete(PaymentRecord paymentRecord);
        void Update(PaymentRecord paymentRecord);
        List<PaymentRecordResModel> GetPaymentRecordsFromBookingId(string bookingId);
        void CancelBookingPaymentRecords(string bookingId);
    }
}
