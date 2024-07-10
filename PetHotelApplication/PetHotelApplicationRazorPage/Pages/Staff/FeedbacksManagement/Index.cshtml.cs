using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.FeedbackService;

namespace PetHotelApplicationRazorPage.Pages.Staff.FeedbacksManagement
{
    public class IndexModel : AuthorizePageModel
    {
        private readonly IFeedbackService _feedbackService;

        public IndexModel(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public PaginatedList<Feedback> Feedback { get; set; } = default!;

        public static int pageSize = 6;

        public async Task OnGetAsync(int? pageIndex)
        {
            var feedbacksList = _feedbackService.GetFeedbacks().OrderByDescending(x => x.Date).ToList();

            Feedback = PaginatedList<Feedback>.Create(feedbacksList, pageIndex ?? 1, pageSize);
        }
    }
}
