using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using BusinessObjects.Entities;
using Services.Services.UserService;
using BusinessObjects.CustomValidators;
using Services.Services.RoleService;
using BusinessObjects.Models.UserModel.Request;

namespace PetHotelApplicationRazorPage.Pages.Admin.UserManagement
{
    public class CreateModel : AuthorizePageModel
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly CloudinaryService _cloudinaryService;

        public CreateModel(IUserService userService, IRoleService roleService, CloudinaryService cloudinaryService)
        {
            _userService = userService;
            _roleService = roleService;
            _cloudinaryService = cloudinaryService;
        }

        public List<Role> Roles { get; set; }

        public IActionResult OnGet()
        {
            Roles = _roleService.GetRoles();
            if (Roles == null || !Roles.Any())
            {
                ModelState.AddModelError("", "No roles available.");
                return Page();
            }

            return Page();
        }

        [BindProperty]
        public CreateUserReqModel User { get; set; } = default!;

        [BindProperty]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Roles = _roleService.GetRoles();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_userService.isEmailExist(User.Email))
            {
                ModelState.AddModelError("User.Email", "Email already exists.");
                return Page();
            }

            if (_userService.isPhoneNumberExist(User.PhoneNumber))
            {
                ModelState.AddModelError("User.PhoneNumber", "Phone number already exists.");
                return Page();
            }

            if (Image != null)
            {
                var uploadResult = await _cloudinaryService.AddPhoto(Image);
                User.Avatar = uploadResult.SecureUrl.ToString();
            }

            _userService.CreateUserReq(User);

            return RedirectToPage("./Index");
        }
    }
}
