using BusinessObjects.Entities;
using BusinessObjects.Enums.StatusEnums;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.PetCareServiceRepo
{
    public class PetCareServiceRepository : IPetCareServiceRepository
    {
        public string Add(PetCareService petCareService)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                _context.PetCareServices.Add(petCareService);
                _context.SaveChanges();
                return petCareService.Id.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(PetCareService petCareService)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                var currPetCareService = _context.PetCareServices.FirstOrDefault(x => x.Id.Equals(petCareService.Id));

                currPetCareService.Status = StatusEnums.Unavailable.ToString();

                _context.Update(currPetCareService);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PetCareService GetPetCareServiceById(string id)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.PetCareServices.Include(x => x.ServiceImages).FirstOrDefault(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PetCareService GetPetCareServiceByType(string type)
        {
            using var _contex = new PetHotelApplicationDbContext();
            return _contex.PetCareServices.Where(x => x.Type.ToLower().Equals(type.ToLower())).FirstOrDefault();
        }

        public List<PetCareService> GetPetCareServices()
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.PetCareServices.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PetCareService> GetPetCareServicesByIds(List<string> Ids)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.PetCareServices.Where(x => Ids.Contains(x.Id)).ToList(); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(PetCareService petCareService)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                //_context.Categories.Update(category);
                _context.Entry<PetCareService>(petCareService).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
