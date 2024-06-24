using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Services.Services.PetService;

namespace PetHotelApplicationRazorPage.Pages.User.Pets
{
    public class EditModel : PageModel
    {
        private readonly IPetService _petService;

        public EditModel(IPetService petService)
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
            Pet = pet;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var pet = _petService.GetPetById(Pet.Id);
            if (pet == null)
            {
                return NotFound();
            }

            try
            {
                _petService.Update(Pet);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
