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
        public void Add(BookingInformation bookingInformation)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
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
                using var _context = new PetHotelApplicationDbContext();
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
                using var _context = new PetHotelApplicationDbContext();
                return _context.BookingInformations.Include(b => b.User).Include(b => b.Pet).Include(b => b.Accommodation).ToList();
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
                using var _context = new PetHotelApplicationDbContext();
                return _context.BookingInformations
                    .Include(x => x.User)
                    .Include(x => x.Accommodation)
                    .Include(x => x.Pet)
                    .Include(x => x.ServiceBookings)
                        .ThenInclude(sb => sb.Service)
                    .FirstOrDefault(x => x.Id.Equals(id));
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
                using var _context = new PetHotelApplicationDbContext();
                //_context.Categories.Update(category);
                _context.Entry<BookingInformation>(bookingInformation).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BookingInformation> GetBookingInformationByUserId(string userId)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.BookingInformations
                            .Include(b => b.User)
                            .Include(b => b.Accommodation)
                            .Include(b => b.Pet)
                            .Include(b => b.ServiceBookings)
                                .ThenInclude(sb => sb.Service)
                            .OrderBy(b => b.Pet.PetName)
                            .OrderByDescending(b => b.StartDate)
                            .Where(b => b.User.Id.Equals(userId))
                            .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
