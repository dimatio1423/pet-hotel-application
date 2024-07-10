using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.CloudinaryService;

namespace PetHotelApplicationRazorPage.Pages
{
    public class UploadModel : PageModel
    {
        private readonly ICloudinaryService _cloudinaryService;

        public UploadModel(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public string ImageUrl { get; private set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ImageFile == null || ImageFile.Length == 0)
            {
                TempData["Error"] = "Please select image to upload";
                return Page();
            }

            var uploadResult = await _cloudinaryService.AddPhoto(ImageFile);
            ImageUrl = uploadResult.SecureUrl.ToString();

            return Page();
        }
    }
}
