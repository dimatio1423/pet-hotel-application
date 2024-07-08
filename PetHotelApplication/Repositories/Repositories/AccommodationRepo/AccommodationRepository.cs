using Azure;
using BusinessObjects.Entities;
using BusinessObjects.Enums.StatusEnums;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.AccommodationRepo
{
    public class AccommodationRepository : IAccommodationRepository
    {
        public void Add(Accommodation accommodation)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
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
                using var _context = new PetHotelApplicationDbContext();
                var currAccommodation = _context.Accommodations.FirstOrDefault(x => x.Id.Equals(accommodation.Id));

                currAccommodation.Status = StatusEnums.Inactive.ToString();

                _context.Update(currAccommodation);
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
                using var _context = new PetHotelApplicationDbContext();
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
                using var _context = new PetHotelApplicationDbContext();
                return _context.Accommodations.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public List<Accommodation> GetAccommodationsWithSearchSort(string searchString, string sortOrder)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                var accommodations = _context.Accommodations.AsQueryable();

                if (!string.IsNullOrEmpty(searchString))
                {
                    accommodations = accommodations.Where(a => a.Name.Contains(searchString) ||
                                                               a.Type.Contains(searchString));
                }

                accommodations = sortOrder switch
                {
                    "Name" => accommodations.OrderBy(a => a.Name),
                    "Type" => accommodations.OrderBy(a => a.Type),
                    "Capacity" => accommodations.OrderBy(a => a.Capacity),
                    "Status" => accommodations.OrderBy(a => a.Status),
                    "Price" => accommodations.OrderBy(a => a.Price),
                    _ => accommodations.OrderBy(a => a.Name),
                };

                return accommodations.ToList();
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
                using var _context = new PetHotelApplicationDbContext();
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
