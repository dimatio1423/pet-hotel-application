using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models.BookingHistoryModel.Response;
using Services.Services.BookingInformationService;

namespace PetHotelApplicationRazorPage.Pages.User.Booking
{
    public class DetailsModel : PageModel
    {
        private readonly IBookingInformationService _bookingInformationService;

        public DetailsModel(IBookingInformationService bookingInformationService)
        {
            _bookingInformationService = bookingInformationService;
        }

        public BookingDetailsResModel BookingDetails { get; set; }

        public IActionResult OnGet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentBooking = _bookingInformationService.GetBookingInformationById(id);

            if (currentBooking == null)
            {
                return NotFound();
            }

            BookingDetails = new BookingDetailsResModel
            {
                Id = currentBooking.Id,
                BoardingType = currentBooking.BoardingType,
                StartDate = currentBooking.StartDate,
                EndDate = currentBooking.EndDate,
                Note = currentBooking.Note,
                Accommodation = new AccommodationResModel
                {
                    Type = currentBooking.Accommodation.Type
                },
                Pet = new PetResModel
                {
                    PetName = currentBooking.Pet.PetName
                },
                PetCareServices = new List<PetCareServiceResModel>()
            };

            foreach (var serviceBooking in currentBooking.ServiceBookings)
            {
                BookingDetails.PetCareServices.Add(new PetCareServiceResModel
                {
                    Type = serviceBooking.Service.Type,
                    Price = serviceBooking.Service.Price
                });
            }

            var totalDays = Math.Max(1, (int)(currentBooking.EndDate - currentBooking.StartDate).TotalDays);
            var totalServicePrice = currentBooking.ServiceBookings.Sum(sb => sb.Service.Price);
            var accommodationPrice = currentBooking.Accommodation.Price;
            BookingDetails.TotalPrice = totalDays * (totalServicePrice + accommodationPrice);

            return Page();
        }
    }
}
