using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Services.Services.PetService;

namespace PetHotelApplicationRazorPage.Pages.User.Pets
{
    public class DeleteModel : PageModel
    {
        private readonly IPetService _petService;

        public DeleteModel(IPetService petService)
        {
            _petService = petService;
        }

        [BindProperty]
        public Pet Pet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = _petService.GetPetById(id);

            if (pet == null)
            {
                return NotFound();
            }
            else
            {
                Pet = pet;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = _petService.GetPetById(id);
            if (pet != null)
            {
                Pet = pet;
                _petService.Delete(Pet);
            }

            return RedirectToPage("./Index");
        }
    }
}
