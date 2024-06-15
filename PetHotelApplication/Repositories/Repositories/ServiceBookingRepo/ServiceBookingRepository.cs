using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.ServiceBookingRepo
{
    public class ServiceBookingRepository : IServiceBookingRepository
    {
        public void Add(ServiceBooking serviceBooking)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                _context.ServiceBookings.Add(serviceBooking);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(ServiceBooking serviceBooking)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                var currServiceBooking = _context.Feedbacks.FirstOrDefault(x => x.Id.Equals(serviceBooking.Id));

                _context.Remove(currServiceBooking);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ServiceBooking> GetServiceBookingsByBookingId(string Id)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.ServiceBookings.Where(x => x.BookingId.Equals(Id)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(ServiceBooking serviceBooking)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                //_context.Categories.Update(category);
                _context.Entry<ServiceBooking>(serviceBooking).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
