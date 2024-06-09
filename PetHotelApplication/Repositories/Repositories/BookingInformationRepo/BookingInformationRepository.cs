using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.BookingInformationRepo
{
    public class BookingInformationRepository : IBookingInformationRepository
    {
        private readonly PetHotelApplicationDbContext _context;

        public BookingInformationRepository(PetHotelApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(BookingInformation bookingInformation)
        {
            try
            {
                _context.BookingInformations.Add(bookingInformation);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(BookingInformation bookingInformation)
        {
            try
            {
                var currBooking = _context.BookingInformations.FirstOrDefault(x => x.Id.Equals(bookingInformation.Id));

                currBooking.Status = "Inactive";

                _context.Update(currBooking);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BookingInformation> GetBookingInformations()
        {
            try
            {
                return _context.BookingInformations.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BookingInformation GetBookingInformationById(string id)
        {
            try
            {
                return _context.BookingInformations.FirstOrDefault(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(BookingInformation bookingInformation)
        {
            try
            {
                //_context.Categories.Update(category);
                _context.Entry<BookingInformation>(bookingInformation).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
