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
using BusinessObjects.Models.PetModel.Response;
using AutoMapper;
using BusinessObjects.Models.BookingInformationModel.Response;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Enums.BookingStatusEnums;

namespace PetHotelApplicationRazorPage.Pages.User.Booking
{
    public class BookingHistoryModel : AuthorizePageModel
    {
        private readonly IBookingInformationService _bookingInformationService;
        private readonly IMapper _mapper;

        public BookingHistoryModel(IBookingInformationService bookingInformationService, IMapper mapper)
        {
            _bookingInformationService = bookingInformationService;
            _mapper = mapper;
        }

        public PaginatedList<BookingInformationResModel> BookingInfos { get; set; } = default!;

        public SelectList BookingStatus { get; set; }

        public string StatusFilter { get; set; }

        public string CurrentFilter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        private const int pageSize = 4;

        public async Task OnGetAsync(string currentFilter, string searchString, int? pageIndex,
            DateTime? startDate, DateTime? endDate, string status)
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

            StartDate = startDate;
            EndDate = endDate;

            StatusFilter = status;

            var currentUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");
            var list = _bookingInformationService.GetBookingInformationByUserId(currentUser.Id)
                                                .AsEnumerable();

            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(e => e.Pet.PetName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(StatusFilter))
            {
                list = list.Where(e => e.Status.Equals(StatusFilter));
            }

            if (startDate.HasValue)
            {
                list = list.Where(e => e.StartDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                list = list.Where(e => e.StartDate <= endDate.Value);
            }

            BookingInfos = PaginatedList<BookingInformationResModel>.Create(list, pageIndex ?? 1, pageSize);

            BookingStatus = new SelectList(Enum.GetNames(typeof(BookingStatusEnums)), StatusFilter);
        }
    }
}
