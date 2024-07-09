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
using BusinessObjects.Enums.BookingStatusEnums;

namespace PetHotelApplicationRazorPage.Pages.Staff.BookingManagement
{
    public class IndexModel : AuthorizePageModel
    {
        private readonly IBookingInformationService _bookingInformationService;

        public IndexModel(IBookingInformationService bookingInformationService)
        {
            _bookingInformationService = bookingInformationService;
        }

        public PaginatedList<BookingInformation> BookingInformation { get;set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            var responseList = _bookingInformationService.GetBookingInformations();
            BookingInformation = PaginatedList<BookingInformation>.Create(responseList, pageIndex ?? 1, 5);
        }

    }
}
