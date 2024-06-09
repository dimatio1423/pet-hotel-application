using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Models.UserModel;
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
            BusinessObjects.Entities.User currUser = _userService.GetUserByEmail(loginModel.Email);
            if (currUser != null)
            {
                var isPasswordValid = _userService.VerifyPassword(loginModel.Password, currUser.Password);
                if (isPasswordValid)
                {
                    return RedirectToPage("/Privacy");
                }
                TempData["Error"] = "Mật khẩu sai. Vui lòng nhập lại";
                return RedirectToPage("./Login");
            }
            TempData["Error"] = "Email không tồn tại";
            return RedirectToPage("./Login");
        }
    }
}

