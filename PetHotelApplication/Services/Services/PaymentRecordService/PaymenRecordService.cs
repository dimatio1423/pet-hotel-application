using BusinessObjects.Entities;
using Repositories.Repositories.PaymentRecordRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.PaymentRecordService
{
    public class PaymenRecordService : IPaymentRecordService
    {
        private readonly IPaymentRecordRepository _paymentRecordRepo;

        public PaymenRecordService(IPaymentRecordRepository paymentRecordRepo)
        {
            _paymentRecordRepo = paymentRecordRepo;
        }
        public void Add(PaymentRecord paymentRecord)
        {
            _paymentRecordRepo.Add(paymentRecord);
        }

        public void Delete(PaymentRecord paymentRecord)
        {
            _paymentRecordRepo.Delete(paymentRecord);
        }

        public PaymentRecord GetPaymentRecordById(string id)
        {
            return _paymentRecordRepo.GetPaymentRecordById(id);
        }

        public List<PaymentRecord> GetPaymentRecords()
        {
            return _paymentRecordRepo.GetPaymentRecords();
        }

        public void Update(PaymentRecord paymentRecord)
        {
            _paymentRecordRepo.Update(paymentRecord);
        }
    }
}
