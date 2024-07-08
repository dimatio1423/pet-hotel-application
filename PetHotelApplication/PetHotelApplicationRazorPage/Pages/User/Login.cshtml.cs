using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models.UserModel;
using Services.Services.UserService;
using BusinessObjects.Enums.RoleEnums;
using BusinessObjects.Enums.StatusEnums;

namespace PetHotelApplicationRazorPage.Pages.User
{
    public class LoginModel : AuthorizePageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public LoginUserReqModel loginModel { get; set; } = default;
        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("Account") != null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var currEmail = loginModel.Email;
            BusinessObjects.Entities.User currUser = _userService.GetUserByEmail(loginModel.Email);
            if (currUser != null)
            {
                if (currUser.Status.Equals(StatusEnums.Inactive.ToString()))
                {
                    return RedirectToPage("/Forbidden");
                }

                var isPasswordValid = _userService.VerifyPassword(loginModel.Password, currUser.Password);
                if (isPasswordValid)
                {
                    SessionHelper.SetObjectSession(HttpContext.Session, "Account", currUser);
                    return RedirectToPage("/Index");
                }
                TempData["Error"] = "Wrong password. Please try again !!!";
                loginModel.Email = currEmail;
                return Page();
            }
            TempData["Error"] = "Account does not exist";
            return Page();
        }
    }
}

