using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Repositories;
using BusinessObjects.Models.BookingModel.Response;
using Services.Services.BookingInformationService;
using BusinessObjects.Models.PetModel.Response;

namespace PetHotelApplicationRazorPage.Pages.User.Booking
{
    public class BookingHistoryModel : PageModel
    {
        private readonly IBookingInformationService _bookingInformationService;

        public BookingHistoryModel(IBookingInformationService bookingInformationService)
        {
            _bookingInformationService = bookingInformationService;
        }

        public PaginatedList<BookingInformationResModel> BookingInfos { get; set; } = default!;

        public string CurrentFilter { get; set; }

        private const int pageSize = 4;

        public async Task OnGetAsync(string currentFilter, string searchString, int? pageIndex)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            var currentUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");
            var list = _bookingInformationService.GetBookingInformationByUserId(currentUser.Id);

            BookingInfos = PaginatedList<BookingInformationResModel>.Create(list, pageIndex ?? 1, pageSize);
        }
    }
}
