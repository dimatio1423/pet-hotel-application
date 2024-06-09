using BusinessObjects.Entities;
using Repositories.Repositories.FeedbackRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepo;

        public FeedbackService(IFeedbackRepository feedbackRepo)
        {
            _feedbackRepo = feedbackRepo;
        }
        public void Add(Feedback feedback)
        {
            _feedbackRepo.Add(feedback);
        }

        public void Delete(Feedback feedback)
        {
            _feedbackRepo.Delete(feedback);
        }

        public Feedback GetFeedbackById(string id)
        {
            return _feedbackRepo.GetFeedbackById(id);
        }

        public List<Feedback> GetFeedbacks()
        {
            return _feedbackRepo.GetFeedbacks();
        }

        public void Update(Feedback feedback)
        {
            _feedbackRepo.Update(feedback);
        }
    }
}
