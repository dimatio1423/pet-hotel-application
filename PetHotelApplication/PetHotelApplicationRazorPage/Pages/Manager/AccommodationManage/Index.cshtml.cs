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

        public async Task OnGetAsync(int? pageIndex)
        {
            var accommodations = _accommodationService.GetAccommodations();
            Accommodation = PaginatedList<Accommodation>.Create(accommodations, pageIndex ?? 1, pageSize);
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
