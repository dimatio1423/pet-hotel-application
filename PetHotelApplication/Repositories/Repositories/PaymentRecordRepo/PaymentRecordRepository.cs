using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.PaymentRecordRepo
{
    public class PaymentRecordRepository : IPaymentRecordRepository
    {
        private readonly PetHotelApplicationDbContext _context;

        public PaymentRecordRepository(PetHotelApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(PaymentRecord paymentRecord)
        {
            try
            {
                var currPaymentRecord = _context.PaymentRecords.FirstOrDefault(x => x.Id.Equals(paymentRecord.Id));

                currPaymentRecord.Status = "Inactive";

                _context.Update(currPaymentRecord);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(PaymentRecord paymentRecord)
        {
            throw new NotImplementedException();
        }

        public PaymentRecord GetPaymentRecordById(string id)
        {
            try
            {
                return _context.PaymentRecords.FirstOrDefault(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PaymentRecord> GetPaymentRecords()
        {
            try
            {
                return _context.PaymentRecords.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(PaymentRecord paymentRecord)
        {
            try
            {
                //_context.Categories.Update(category);
                _context.Entry<PaymentRecord>(paymentRecord).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
