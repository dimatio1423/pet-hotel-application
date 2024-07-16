using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.UserService;
using BusinessObjects.Models.UserModel.Request;

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
            var currUserPhoneNumber = _userService.GetUsers("", null).FirstOrDefault(x => x.PhoneNumber.Equals(register.PhoneNumber));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (currUser != null)
            {
                TempData["Error"] = "Email is already taken";
                return Page();
            }

            if (currUserPhoneNumber != null)
            {
                TempData["Error"] = "Phone Number is already taken";
                return Page();
            }

            _userService.RegisterUser(register);
            return RedirectToPage("./Login");
        }
    }
}
