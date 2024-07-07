using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Services.UserService;
using BusinessObjects.Enums.StatusEnums;

namespace PetHotelApplicationRazorPage.Pages.Admin.UserManagement
{
    public class ChangeStatusModel : AuthorizePageModel
    {
        private readonly IUserService _userService;

        public ChangeStatusModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public BusinessObjects.Entities.User User { get; set; } = default!;

        [BindProperty]
        public string UserId { get; set; } = default!;

        [BindProperty]
        public string Status { get; set; } = default!;

        public IActionResult OnGet(string id, string status)
        {
            if (id == null || status == null)
            {
                return NotFound();
            }

            User = _userService.GetUserById(id);
            if (User == null)
            {
                return NotFound();
            }

            UserId = id;
            Status = status;

            return Page();
        }

        public IActionResult OnPost()
        {
            var user = _userService.GetUserById(UserId);
            if (user == null)
            {
                return NotFound();
            }

            user.Status = Status;
            _userService.UpdateUser(user);

            return RedirectToPage("./Index");
        }
    }
}
