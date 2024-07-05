using BusinessObjects.Entities;
using BusinessObjects.Models.BookingInformationModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.BookingInformationService
{
    public interface IBookingInformationService
    {
        List<BookingInformation> GetBookingInformations();
        BookingInformation GetBookingInformationById(string id);
        void Add(BookingInformation bookingInformation);
        void Delete(BookingInformation bookingInformation);
        void Update(BookingInformation bookingInformation);
        List<BookingInformationResModel> GetBookingInformationByUserId(string userId);
    }
}
