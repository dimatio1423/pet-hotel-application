using BusinessObjects.Entities;
using Repositories.Repositories.BookingInformationRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.BookingInformationService
{
    public class BookingInformatonService : IBookingInformationService
    {
        private readonly IBookingInformationRepository _bookingInformationRepo;

        public BookingInformatonService(IBookingInformationRepository bookingInformationRepo)
        {
            _bookingInformationRepo = bookingInformationRepo;
        }
        public void Add(BookingInformation bookingInformation)
        {
            _bookingInformationRepo.Add(bookingInformation);
        }

        public void Delete(BookingInformation bookingInformation)
        {
            _bookingInformationRepo.Delete(bookingInformation);
        }

        public BookingInformation GetBookingInformationById(string id)
        {
           return _bookingInformationRepo.GetBookingInformationById(id);
        }

        public List<BookingInformation> GetBookingInformations()
        {
            return _bookingInformationRepo.GetBookingInformations();
        }

        public void Update(BookingInformation bookingInformation)
        {
            _bookingInformationRepo.Update(bookingInformation);
        }
    }
}
