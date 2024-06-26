using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.PetCareServices;

namespace PetHotelApplicationRazorPage.Pages.Staff.ServicesManagement
{
    public class DeleteModel : AuthorizePageModel
    {
        private readonly IPetCareService _petCareService;

        public DeleteModel(IPetCareService petCareService)
        {
            _petCareService = petCareService;
        }

        [BindProperty]
        public PetCareService PetCareService { get; set; } = default!;

        public List<ServiceImage> ServiceImages { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petcareservice = _petCareService.GetPetCareServiceById(id);

            if (petcareservice == null)
            {
                return NotFound();
            }
            else
            {
                PetCareService = petcareservice;
                ServiceImages = petcareservice.ServiceImages.ToList();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petcareservice = _petCareService.GetPetCareServiceById(id);
            if (petcareservice != null)
            {
                PetCareService = petcareservice;
                _petCareService.Delete(petcareservice);
            }

            return RedirectToPage("./Index");
        }
    }
}
