
using BusinessObjects.Entities;
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

                currPet.Status = "Inactive";

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
    }
}
