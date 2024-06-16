using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models.UserModel;
using Services.Services.UserService;

namespace PetHotelApplicationRazorPage.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public LoginUserReqModel loginModel { get; set; } = default;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var currEmail = loginModel.Email;
            BusinessObjects.Entities.User currUser = _userService.GetUserByEmail(loginModel.Email);
            if (currUser != null)
            {
                var isPasswordValid = _userService.VerifyPassword(loginModel.Password, currUser.Password);
                if (isPasswordValid)
                {
                    HttpContext.Session.SetString("AccountEmail", currUser.Email);
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

