
using BusinessObjects.Entities;
using BusinessObjects.Enums.StatusEnums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.PetRepo
{
    public class PetRepository : IPetRepository
    {
        public void Add(Pet pet)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();                

                _context.Pets.Add(pet);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Pet pet)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                var currPet = _context.Pets.FirstOrDefault(x => x.Id.Equals(pet.Id));

                currPet.Status = StatusEnums.Inactive.ToString();

                _context.Update(currPet);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Pet GetPetById(string id)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.Pets.FirstOrDefault(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Pet? GetPetDetailsById(string id)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.Pets
                            .Include(p => p.BookingInformations)
                                .ThenInclude(b => b.Accommodation)
                            .Include(p => p.BookingInformations)
                                .ThenInclude(b => b.PaymentRecords)                                
                            .Include(p => p.BookingInformations)
                                .ThenInclude(b => b.ServiceBookings)
                                    .ThenInclude(s => s.Service)
                            .FirstOrDefault(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Pet> GetPets()
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.Pets.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Pet pet)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                //_context.Categories.Update(category);
                _context.Entry<Pet>(pet).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Pet> GetActivePets(string userId, string petName)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.Pets
                        .Include(p => p.User)
                        .Where(p => p.UserId.Equals(userId) && 
                                    p.Status.Equals(nameof(StatusEnums.Active)) && 
                                    (string.IsNullOrEmpty(petName) || p.PetName.Contains(petName))
                              )
                        .OrderBy(p => p.PetName)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
