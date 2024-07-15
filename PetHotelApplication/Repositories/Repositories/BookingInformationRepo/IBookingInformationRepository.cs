using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.BookingInformationRepo
{
    public interface IBookingInformationRepository
    {
        List<BookingInformation> GetBookingInformations();
        BookingInformation GetBookingInformationById(string id);
        void Add(BookingInformation bookingInformation);
        void Delete(BookingInformation bookingInformation);
        void Update(BookingInformation bookingInformation);
        List<BookingInformation> GetBookingInformationByUserId(string userId);
        void UpdateRange(List<BookingInformation> list);
    }
}
