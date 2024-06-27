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

namespace PetHotelApplicationRazorPage.Pages.Manager.AccommodationManage
{
    public class DetailsModel : PageModel
    {
        private readonly IAccommodationService _accommodationService;

        public DetailsModel(IAccommodationService accommodationService)
        {
            _accommodationService = accommodationService;
        }

        public Accommodation Accommodation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accommodation = _accommodationService.GetAccommodationById(id);
            if (accommodation == null)
            {
                return NotFound();
            }
            else
            {
                Accommodation = accommodation;
            }
            return Page();
        }
    }
}
