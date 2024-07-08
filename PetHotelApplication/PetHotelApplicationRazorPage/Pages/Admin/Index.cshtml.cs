using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.DashboardService;

namespace PetHotelApplicationRazorPage.Pages.Admin
{
    public class IndexModel : AuthorizePageModel
    {
        private readonly IDashboardService _dashboardService;

        public IndexModel(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public int TotalUsers { get; private set; }
        public int TotalBookings { get; private set; }
        public int TotalServices { get; private set; }
        public int TotalAccommodations { get; private set; }
        public int[] FeedbackCounts { get; private set; }

        public IActionResult OnGet()
        {
            TotalUsers = _dashboardService.GetTotalUsers();
            TotalBookings = _dashboardService.GetTotalBookings();
            TotalServices = _dashboardService.GetTotalServices();
            TotalAccommodations = _dashboardService.GetTotalAccommodations();
            FeedbackCounts = _dashboardService.GetFeedbackCountsByRating();

            return Page();
        }
    }
}