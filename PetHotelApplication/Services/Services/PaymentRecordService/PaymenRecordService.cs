using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Enums.PaymenStatusEnums;
using BusinessObjects.Models.PaymentRecordModel.Response;
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
        private readonly IMapper _mapper;

        public PaymenRecordService(IPaymentRecordRepository paymentRecordRepo, IMapper mapper)
        {
            _paymentRecordRepo = paymentRecordRepo;
            _mapper = mapper;
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

        public List<PaymentRecordResModel> GetPaymentRecordsFromBookingId(string bookingId)
        {
            var list = _paymentRecordRepo.GetPaymentRecordsFromBookingId(bookingId);
            return _mapper.Map<List<PaymentRecordResModel>>(list);
        }

        public void CancelBookingPaymentRecords(string bookingId)
        {
            var list = _paymentRecordRepo.GetPaymentRecordsFromBookingId(bookingId);
            list.Where(p => p.Status.Equals(nameof(PaymentStatusEnums.Unpaid)))
                .ToList()
                .ForEach(p => p.Status = nameof(PaymentStatusEnums.Canceled));
            _paymentRecordRepo.UpdateRange(list);
        }
    }
}
