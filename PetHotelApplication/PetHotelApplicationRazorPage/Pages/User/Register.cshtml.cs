using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Models.UserModel;
using Services.Services.UserService;

namespace PetHotelApplicationRazorPage.Pages.User
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public RegisterUserReqModel register { get; set; } = default;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var currUser = _userService.GetUserByEmail(register.Email);
            if (currUser != null)
            {
                TempData["Error"] = "Email đã tồn tại";
                return RedirectToPage("./Register");
            }

            _userService.RegisterUser(register);
            return RedirectToPage("./Login");
        }
    }
}
