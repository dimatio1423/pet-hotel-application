using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.FeedbackService;

namespace PetHotelApplicationRazorPage.Pages.User.Feedbacks
{
    public class IndexModel : AuthorizePageModel
    {
        private readonly IFeedbackService _feedbackService;

        public IndexModel(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public PaginatedList<Feedback> Feedback { get;set; } = default!;

        public static int pageSize = 6;

        public async Task OnGetAsync(int? pageIndex)
        {
            var feedbacksList = _feedbackService.GetFeedbacks().OrderByDescending(x => x.Date).ToList();

            Feedback = PaginatedList<Feedback>.Create(feedbacksList, pageIndex ?? 1, pageSize);
        }
    }
}
