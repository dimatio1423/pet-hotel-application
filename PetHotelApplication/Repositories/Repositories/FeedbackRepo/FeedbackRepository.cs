using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.FeedbackRepo
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly PetHotelApplicationDbContext _context;

        public FeedbackRepository(PetHotelApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Feedback feedback)
        {
            try
            {
                _context.Feedbacks.Add(feedback);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Feedback feedback)
        {
            try
            {
                var currFeedback = _context.Feedbacks.FirstOrDefault(x => x.Id.Equals(feedback.Id));

                _context.Remove(currFeedback);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Feedback GetFeedbackById(string id)
        {
            try
            {
                return _context.Feedbacks.FirstOrDefault(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Feedback> GetFeedbacks()
        {
            try
            {
                return _context.Feedbacks.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Feedback feedback)
        {
            try
            {
                //_context.Categories.Update(category);
                _context.Entry<Feedback>(feedback).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
