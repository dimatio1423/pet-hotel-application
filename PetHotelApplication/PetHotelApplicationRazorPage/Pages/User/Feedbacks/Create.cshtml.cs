using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.FeedbackService;
using BusinessObjects.Models.FeedbackModel.Request;

namespace PetHotelApplicationRazorPage.Pages.User.Feedbacks
{
    public class CreateModel : AuthorizePageModel
    {
        private readonly IFeedbackService _feedbackService;

        public CreateModel(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FeedbackCreateReqModel Feedback { get; set; } = default!;

        public BusinessObjects.Entities.User currentUser { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");
            if (currUser != null)
            {
                currentUser = currUser;
            }

            Feedback feedback = new Feedback
            {
                Id = Guid.NewGuid().ToString(),
                Comment = Feedback.Comment,
                Rating = Feedback.Rating,
                Date = DateTime.Now,
                UserId = currentUser.Id
            };

            _feedbackService.Add(feedback);

            return RedirectToPage("./Index");
        }
    }
}
