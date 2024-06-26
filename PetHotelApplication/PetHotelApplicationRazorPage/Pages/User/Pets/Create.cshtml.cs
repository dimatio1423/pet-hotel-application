using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Entities;
using Services.Services.PetService;
using AutoMapper;
using BusinessObjects.Models.PetModel.Request;
using BusinessObjects.CustomValidators;

namespace PetHotelApplicationRazorPage.Pages.User.Pets
{
    public class CreateModel : AuthorizePageModel
    {
        private readonly IPetService _petService;
        private readonly IMapper _mapper;
        private readonly CloudinaryService _cloudinaryService;

        public CreateModel(IPetService petService, IMapper mapper, CloudinaryService cloudinaryService)
        {
            _petService = petService;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PetCreateReqModel Pet { get; set; } = default!;

        [BindProperty]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile? Image { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
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
                var currentUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");
            
                var newPet = _mapper.Map<Pet>(Pet);
                newPet.UserId = currentUser.Id;

                _petService.Add(newPet);
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
