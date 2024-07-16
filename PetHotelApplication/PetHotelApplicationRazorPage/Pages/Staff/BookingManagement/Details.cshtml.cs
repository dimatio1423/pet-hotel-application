using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.BookingInformationService;
using AutoMapper;
using BusinessObjects.Enums.BookingStatusEnums;
using BusinessObjects.Models.BookingInformationModel.Response;
using Services.Services.PaymentRecordService;
using Services.Services.VnPaymentServices;

namespace PetHotelApplicationRazorPage.Pages.Staff.BookingManagement
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

        public BookingContinuePaymentResModel BookingInfo { get; set; }

        [BindProperty]
        public string BookingId { get; set; }

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

            BookingInfo = _mapper.Map<BookingContinuePaymentResModel>(currentBooking);
            BookingId = BookingInfo.Id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            try
            {
                var currBooking = _bookingInformationService.GetBookingInformationById(id);
                if (currBooking == null)
                {
                    return NotFound();
                }
                else
                {
                    currBooking.Status = BookingStatusEnums.Completed.ToString();
                    currBooking.EndDate = DateTime.Now;

                    _bookingInformationService.Update(currBooking);
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExisted(BookingInfo.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Staff/BookingManagement/Index");
        }

        private bool BookingExisted(string id)
        {
            return _bookingInformationService.GetBookingInformationById(id) != null ? true : false;
        }
    }
}
