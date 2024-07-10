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
using BusinessObjects.Models.BookingInformationModel.Response;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Enums.PaymenStatusEnums;

namespace PetHotelApplicationRazorPage.Pages.Staff.BookingManagement
{
    public class IndexModel : AuthorizePageModel
    {
        private readonly IBookingInformationService _bookingInformationService;

        public IndexModel(IBookingInformationService bookingInformationService)
        {
            _bookingInformationService = bookingInformationService;
        }

        public PaginatedList<BookingListInformationResModel> BookingInformation { get; set; } = default!;
        public SelectList BookingStatus { get; set; }
        public string StatusFilter { get; set; }
        public string CurrentFilter { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public DateTime? EndDateFrom { get; set; }
        public DateTime? EndDateTo { get; set; }
        public async Task OnGetAsync(int? pageIndex, string currentFilter, string searchString, DateTime? startDateFrom, DateTime? startDateTo, DateTime? endDateFrom, DateTime? endDateTo, string status)
        {
            var responseList = _bookingInformationService.GetBookingInformations().OrderByDescending(b => b.StartDate).ToList();
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            StartDateFrom = startDateFrom;
            StartDateTo = startDateTo;
            EndDateFrom = endDateFrom;
            EndDateTo = endDateTo;

            StatusFilter = status;

            if (!string.IsNullOrEmpty(searchString))
            {
                responseList = responseList.Where(e => e.User.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(StatusFilter))
            {
                responseList = responseList.Where(e => e.Status.Equals(StatusFilter)).ToList();
            }

            if (startDateFrom.HasValue)
            {
                responseList = responseList.Where(e => e.StartDate >= startDateFrom.Value).ToList();
            }

            if (startDateTo.HasValue)
            {
                responseList = responseList.Where(e => e.StartDate <= startDateTo.Value).ToList();
            }

            if (endDateFrom.HasValue)
            {
                responseList = responseList.Where(e => e.EndDate >= endDateFrom.Value).ToList();
            }

            if (endDateTo.HasValue)
            {
                responseList = responseList.Where(e => e.EndDate <= endDateTo.Value).ToList();
            }

            var bookingList = responseList.Select(r => new BookingListInformationResModel
            {
                Id = r.Id,
                BoardingType = r.BoardingType,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                Status = r.Status,
                Email = r.User.Email,
                PetName = r.Pet.PetName
            }).ToList();

            BookingInformation = PaginatedList<BookingListInformationResModel>.Create(bookingList, pageIndex ?? 1, 5);
            BookingStatus = new SelectList(Enum.GetNames(typeof(BookingStatusEnums)), StatusFilter);
        }

    }
}
