using BusinessObjects.Enums.RoleEnums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PetHotelApplicationRazorPage.Pages.Manager
{
    public class IndexModel : AuthorizePageModel
    {
        public async Task<IActionResult> OnGet()
        {
            return Page();
        }
    }
}
