using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Services.UserService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using BusinessObjects.CustomValidators;
using Services.Services.CloudinaryService;
using BusinessObjects.Models.UserModel.Request;

namespace PetHotelApplicationRazorPage.Pages.Admin.UserManagement
{
    public class EditModel : AuthorizePageModel
    {
        private readonly IUserService _userService;
        private readonly ICloudinaryService _cloudinaryService;

        public EditModel(IUserService userService, ICloudinaryService cloudinaryService)
        {
            _userService = userService;
            _cloudinaryService = cloudinaryService;
        }

        [BindProperty]
        public UpdateUserReqModel User { get; set; } = default!;

        [BindProperty]
        public string UserId { get; set; } = default!;

        [BindProperty]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile? Image { get; set; }

        public IActionResult OnGet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEntity = _userService.GetUserById(id);
            if (userEntity == null)
            {
                return NotFound();
            }

            UserId = id;
            User = new UpdateUserReqModel
            {
                FullName = userEntity.FullName,
                PhoneNumber = userEntity.PhoneNumber,
                Address = userEntity.Address,
                Avatar = userEntity.Avatar,
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userEntity = _userService.GetUserById(UserId);
            if (userEntity == null)
            {
                return NotFound();
            }

            if (Image != null)
            {
                var uploadResult = await _cloudinaryService.AddPhoto(Image);
                User.Avatar = uploadResult.SecureUrl.ToString();
            }
            else
            {
                User.Avatar = userEntity.Avatar;
            }

            if (_userService.isPhoneNumberExist(User.PhoneNumber) && userEntity.PhoneNumber != User.PhoneNumber)
            {
                ModelState.AddModelError("User.PhoneNumber", "Phone number already exists.");
                return Page();
            }

            userEntity.FullName = User.FullName;
            userEntity.PhoneNumber = User.PhoneNumber;
            userEntity.Address = User.Address;
            userEntity.Avatar = User.Avatar;

            try
            {
                _userService.UpdateUser(userEntity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_userService.GetUserById(userEntity.Id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}