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
    public class DeleteModel : AuthorizePageModel
    {
        private readonly IFeedbackService _feedbackService;

        public DeleteModel(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = default!;

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
                Feedback = feedback;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = _feedbackService.GetFeedbackById(id);
            if (feedback != null)
            {
                _feedbackService.Delete(feedback);
            }

            return RedirectToPage("/User/Feedbacks/Index");
        }
    }
}
