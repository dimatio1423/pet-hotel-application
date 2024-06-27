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
using BusinessObjects.Models.FeedbackModel.Request;

namespace PetHotelApplicationRazorPage.Pages.User.Feedbacks
{
    public class DetailsModel : AuthorizePageModel
    {
        private readonly IFeedbackService _feedbackService;

        public DetailsModel(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [BindProperty]
        public FeedbackUpdateReqModel Feedback { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");

            var feedback = _feedbackService.GetFeedbackById(id);

            if (currUser != null)
            {
                if (!currUser.Id.Equals(feedback.UserId))
                {
                    return RedirectToPage("/Forbidden");
                }
            }

            if (feedback == null)
            {
                return NotFound();
            }
            else
            {
                Feedback = new FeedbackUpdateReqModel
                {
                    Id = feedback.Id,
                    Comment = feedback.Comment,
                    Rating = feedback.Rating
                };
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currFeedback = _feedbackService.GetFeedbackById(Feedback.Id);

            currFeedback.Comment = Feedback.Comment;
            currFeedback.Rating = Feedback.Rating;
            currFeedback.Date = DateTime.Now;

            _feedbackService.Update(currFeedback);

            return Redirect("/User/Feedbacks/Index");
        }
    }
}
