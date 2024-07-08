using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.AccommodationService;
using BusinessObjects.Enums.StatusEnums;

namespace PetHotelApplicationRazorPage.Pages.Manager.AccommodationManage
{
    public class IndexModel : AuthorizePageModel
    {
        private readonly IAccommodationService _accommodationService;

        private readonly int pageSize = 4;

        public IndexModel(IAccommodationService accommodationService)
        {
            _accommodationService = accommodationService;
        }

        public PaginatedList<Accommodation> Accommodation { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; } = string.Empty;

        public async Task OnGetAsync(int? pageIndex, string? searchString, string? sortOrder)
        {
            SearchString = searchString ?? SearchString;
            SortOrder = sortOrder ?? SortOrder;

            var accommodations = _accommodationService.GetAccommodationsWithSearchSort(SearchString, SortOrder);
            Accommodation = PaginatedList<Accommodation>.Create(accommodations, pageIndex ?? 1, pageSize);
        }

        public bool IsSelected(string value)
        {
            return SortOrder == value;
        }

        public async Task<IActionResult> OnPostDelete(string id, int pageIndex)
        {
            var accommodation = _accommodationService.GetAccommodationById(id);
            accommodation.Status = StatusEnums.Inactive.ToString();
            _accommodationService.Update(accommodation);
            return RedirectToPage(new { pageIndex });
        }

        public async Task<IActionResult> OnPostActive(string id, int pageIndex)
        {
            var accommodation = _accommodationService.GetAccommodationById(id);
            accommodation.Status = StatusEnums.Available.ToString();
            _accommodationService.Update(accommodation);
            return RedirectToPage(new { pageIndex });
        }
    }
}
