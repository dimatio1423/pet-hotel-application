using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Services.UserService;
using Services.Services.RoleService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using BusinessObjects.Models.UserModel;
using BusinessObjects.CustomValidators;

namespace PetHotelApplicationRazorPage.Pages.Admin.UserManagement
{
    public class EditModel : AuthorizePageModel
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly CloudinaryService _cloudinaryService;

        public EditModel(IUserService userService, IRoleService roleService, CloudinaryService cloudinaryService)
        {
            _userService = userService;
            _roleService = roleService;
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

        public List<Role> Roles { get; set; }

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
                RoleId = userEntity.RoleId
            };

            Roles = _roleService.GetRoles();
            if (Roles == null || !Roles.Any())
            {
                ModelState.AddModelError("", "No roles available.");
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Roles = _roleService.GetRoles();
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

            userEntity.FullName = User.FullName;
            userEntity.PhoneNumber = User.PhoneNumber;
            userEntity.Address = User.Address;
            userEntity.Avatar = User.Avatar;
            userEntity.RoleId = User.RoleId;

            if (_userService.isPhoneNumberExist(User.PhoneNumber) && userEntity.PhoneNumber != User.PhoneNumber)
            {
                ModelState.AddModelError("User.PhoneNumber", "Phone number already exists.");
                Roles = _roleService.GetRoles();
                return Page();
            }

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