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
        public void Add(PaymentRecord paymentRecord)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
               
                _context.Add(paymentRecord);

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
                using var _context = new PetHotelApplicationDbContext();
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
                using var _context = new PetHotelApplicationDbContext();
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
                using var _context = new PetHotelApplicationDbContext();
                //_context.Categories.Update(category);
                _context.Entry<PaymentRecord>(paymentRecord).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PaymentRecord> GetPaymentRecordsFromBookingId(string bookingId)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.PaymentRecords
                            .Include(p => p.Booking)
                            .Where(p => p.Booking.Id.Equals(bookingId))
                            .OrderByDescending(p => p.Date)
                            .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
