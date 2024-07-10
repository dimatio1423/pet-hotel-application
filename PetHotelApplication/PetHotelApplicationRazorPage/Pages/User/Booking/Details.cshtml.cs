using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models.BookingHistoryModel.Response;
using Services.Services.BookingInformationService;
using BusinessObjects.Models.BookingInformationModel.Response;
using AutoMapper;

namespace PetHotelApplicationRazorPage.Pages.User.Booking
{
    public class DetailsModel : AuthorizePageModel
    {
        private readonly IBookingInformationService _bookingInformationService;
        private readonly IMapper _mapper;

        public DetailsModel(IBookingInformationService bookingInformationService, IMapper mapper)
        {
            _bookingInformationService = bookingInformationService;
            _mapper = mapper;
        }

        public BookingContinuePaymentResModel BookingDetails { get; set; }

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

            BookingDetails = _mapper.Map<BookingContinuePaymentResModel>(currentBooking);

            return Page();
        }
    }
}
