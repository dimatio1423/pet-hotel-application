using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.PetService;
using BusinessObjects.Models.PetModel.Response;
using BusinessObjects.CustomValidators;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetHotelApplicationRazorPage.Pages.User.Pets
{
    public class DetailsModel : AuthorizePageModel
    {
        private readonly IPetService _petService;
        private readonly CloudinaryService _cloudinaryService;

        public DetailsModel(IPetService petService, CloudinaryService cloudinaryService)
        {
            _petService = petService;
            _cloudinaryService = cloudinaryService;
        }

        [BindProperty]
        public PetResModel Pet { get; set; } = default!;

        [BindProperty]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile? Image { get; set; }

        public SelectList PetSpecies { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            PetSpecies = new SelectList(_petService.GetPetSpecies(), "Name", "Name");

            if (id == null)
            {
                return NotFound();
            }

            var pet = _petService.GetPetDetailsById(id);
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

        public async Task<IActionResult> OnPostAsync()
        {
            PetSpecies = new SelectList(_petService.GetPetSpecies(), "Name", "Name");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Image != null)
            {
                var uploadResult = await _cloudinaryService.AddPhoto(Image);
                Pet.Avatar = uploadResult.SecureUrl.ToString();
            }

            try
            {
                _petService.Update(Pet);
            }
            catch (Exception ex)
            {
                if (Image != null)
                {
                    await _cloudinaryService.DeletePhoto(Pet.Avatar);
                }

                ModelState.AddModelError("", ex.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
