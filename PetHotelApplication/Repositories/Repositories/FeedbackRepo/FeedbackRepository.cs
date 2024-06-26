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
        public void Add(Feedback feedback)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
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
                using var _context = new PetHotelApplicationDbContext();
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
                using var _context = new PetHotelApplicationDbContext();
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
                using var _context = new PetHotelApplicationDbContext();
                return _context.Feedbacks.Include(x => x.User).ToList();
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
                using var _context = new PetHotelApplicationDbContext();
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
