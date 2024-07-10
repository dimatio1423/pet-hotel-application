using BusinessObjects.CustomValidators;
using BusinessObjects.Entities;
using BusinessObjects.Models.UserModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Services.CloudinaryService;
using Services.Services.UserService;
using System.Net.WebSockets;

namespace PetHotelApplicationRazorPage.Pages.User.Profiles
{
    public class EditModel : AuthorizePageModel
    {
        private readonly IUserService _userService;
        private readonly ICloudinaryService _cloudinaryService;

        [BindProperty]
        public UpdateProfileReqModel Profile { get; set; } = default!;

        [BindProperty]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile? Image { get; set; }

        public EditModel(IUserService userService, ICloudinaryService cloudinaryService)
        {
            _userService = userService;
            _cloudinaryService = cloudinaryService;
        }
        public IActionResult OnGet()
        {
            var user = SessionHelper.GetObjectSession<BusinessObjects.Entities.User>(HttpContext.Session, "Account"); 
            if (user != null)
            {
                var currUser = _userService.GetUserById(user.Id);
                Profile = new UpdateProfileReqModel
                {
                    UserId = currUser.Id,
                    FullName = currUser.FullName,
                    PhoneNumber = currUser.PhoneNumber,
                    Address = currUser.Address,
                    Avatar = currUser.Avatar,
                    Email = currUser.Email
                };
            }else
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currUser = _userService.GetUserById(Profile.UserId);
            if (currUser is null)
            {
                return NotFound();
            }

            if (Image != null)
            {
                var uploadResult = await _cloudinaryService.AddPhoto(Image);
                Profile.Avatar = uploadResult.SecureUrl.ToString();
            }
            else
            {
                Profile.Avatar = currUser.Avatar;
            }

            if (!string.IsNullOrEmpty(Profile.Password))
            {
                Profile.Password = _userService.HashPassword(Profile.Password);
            }else
            {
                Profile.Password = currUser.Password;
            }

            currUser.FullName = Profile.FullName;
            currUser.Address = Profile.Address;
            currUser.PhoneNumber = Profile.PhoneNumber;
            currUser.Avatar = Profile.Avatar;
            currUser.Password = Profile.Password;

            if (_userService.isPhoneNumberExist(Profile.PhoneNumber) && currUser.PhoneNumber != Profile.PhoneNumber)
            {
                ModelState.AddModelError("User.PhoneNumber", "Phone number already exists.");
                return Page();
            }

            try
            {
                _userService.UpdateUser(currUser);
                TempData["UpdateSuccess"] = "Update profile successfully";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_userService.GetUserById(currUser.Id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/User/Profiles/Edit");

        }
    }
}
