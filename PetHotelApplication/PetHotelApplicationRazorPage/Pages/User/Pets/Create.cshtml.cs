using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Entities;
using Services.Services.PetService;

namespace PetHotelApplicationRazorPage.Pages.User.Pets
{
    public class CreateModel : AuthorizePageModel
    {
        private readonly IPetService _petService;

        public CreateModel(IPetService petService)
        {
            _petService = petService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Pet Pet { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _petService.Add(Pet);

            return RedirectToPage("./Index");
        }
    }
}
