using Microsoft.AspNetCore.Mvc;

namespace PetHotelApplicationRazorPage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
