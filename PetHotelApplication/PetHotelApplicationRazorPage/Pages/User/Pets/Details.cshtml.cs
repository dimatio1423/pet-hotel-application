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
using Services.Utils;
using BusinessObjects.Enums.StatusEnums;
using Services.Services.CloudinaryService;

namespace PetHotelApplicationRazorPage.Pages.User.Pets
{
    public class DetailsModel : AuthorizePageModel
    {
        private readonly IPetService _petService;
        private readonly ICloudinaryService _cloudinaryService;

        public DetailsModel(IPetService petService, ICloudinaryService cloudinaryService)
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
        public SelectList Status { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            LoadSelectList();

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
            LoadSelectList();

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
                _petService.Update(Utils.TrimWhiteSpace(Pet));
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

        private void LoadSelectList()
        {
            PetSpecies = new SelectList(_petService.GetPetSpecies(), "Name", "Name");
            Status = new SelectList(new List<string>
            {
                StatusEnums.Active.ToString(),
                StatusEnums.Inactive.ToString(),
            });            
        }
    }
}
