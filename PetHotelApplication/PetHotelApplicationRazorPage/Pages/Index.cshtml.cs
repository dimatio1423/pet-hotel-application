using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.UserService;

namespace PetHotelApplicationRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        private readonly ILogger<IndexModel> _logger;
        private readonly IUserService _userService;

        [BindProperty]
        public BusinessObjects.Entities.User currentUser { get; set; }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("AccountEmail") != null)
            {
                var currentAccountEmail = HttpContext.Session.GetString("AccountEmail");
                var currUser = _userService.GetUserByEmail(currentAccountEmail);
                if (currUser != null)
                {
                    currentUser = currUser;
                }
            }
        }
    }
}
