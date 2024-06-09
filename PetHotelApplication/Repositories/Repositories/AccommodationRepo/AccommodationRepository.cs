using Azure;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.AccommodationRepo
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private readonly PetHotelApplicationDbContext _context;

        public AccommodationRepository(PetHotelApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Accommodation accommodation)
        {
            try
            {
                _context.Accommodations.Add(accommodation);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Accommodation accommodation)
        {
            try
            {
                var currAccommodation = _context.Accommodations.FirstOrDefault(x => x.Id.Equals(accommodation.Id));
                _context.Remove(currAccommodation);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Accommodation GetAccommodationById(string id)
        {
            try
            {
                return _context.Accommodations.FirstOrDefault(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Accommodation> GetAccommodations()
        {
            try
            {
                return _context.Accommodations.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Accommodation accommodation)
        {
            try
            {
                //_context.Categories.Update(category);
                _context.Entry<Accommodation>(accommodation).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
