using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.PetCareServices;
using BusinessObjects.Models.PetCareServiceModel.Request;
using Services.Services.ServiceImageService;
using System.ComponentModel.DataAnnotations;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using BusinessObjects.CustomValidators;
using CloudinaryDotNet;
using BusinessObjects.Enums.StatusEnums;
using Services.Services.CloudinaryService;

namespace PetHotelApplicationRazorPage.Pages.Staff.ServicesManagement
{
    public class CreateModel : AuthorizePageModel
    {
        private readonly IPetCareService _petCareService;
        private readonly IServiceImageService _serviceImageService;
        private readonly ICloudinaryService _cloudinaryService;

        public CreateModel(IPetCareService petCareService, 
            IServiceImageService serviceImageService,
            ICloudinaryService cloudinaryService )
        {
            _petCareService = petCareService;
            _serviceImageService = serviceImageService;
            _cloudinaryService = cloudinaryService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PetCareServiceCreateReqMode PetCareService { get; set; } = default!;

        [BindProperty]
        [MinLength(1, ErrorMessage ="Please choose at least one image for service")]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public List<IFormFile> serviceImages { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currPetCareService = _petCareService.GetPetCareServiceByType(PetCareService.Type);

            if (currPetCareService != null)
            {
                TempData["Error"] = "Pet care service type is already existed";

                return Page();
            }

            if (serviceImages.Count > 10)
            {
                TempData["ErrorServiceImage"] = "Maximum ten images for services";

                return Page();
            }

            PetCareService newPetCareService = new PetCareService
            {
                Id = Guid.NewGuid().ToString(),
                Type = PetCareService.Type,
                Description = PetCareService.Description,
                Price = PetCareService.Price,
                Status = StatusEnums.Available.ToString()
            };

            var petCareServiceId = _petCareService.Add(newPetCareService);

            foreach (var image in serviceImages)
            {
                var result = await _cloudinaryService.AddPhoto(image);

                ServiceImage serviceImage = new ServiceImage
                {
                    Id = Guid.NewGuid().ToString(),
                    Image = result.SecureUrl.ToString(),
                    ServiceId = petCareServiceId
                };

                _serviceImageService.Add(serviceImage);
            }

            return RedirectToPage("./Index");
        }
    }
}
