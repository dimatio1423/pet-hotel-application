using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.ServiceImageRepo
{
    public class ServiceImageRepository : IServiceImageRepository
    {
        public void Add(ServiceImage serviceImage)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                _context.ServiceImages.Add(serviceImage);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(ServiceImage serviceImage)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                var currServiceImage = _context.ServiceImages.FirstOrDefault(x => x.Id.Equals(serviceImage.Id));

                _context.Remove(currServiceImage);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ServiceImage> GetServiceImagesByServiceId(string Id)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.ServiceImages.Where(x => x.ServiceId.Equals(Id)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(ServiceImage serviceImage)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                //_context.Categories.Update(category);
                _context.Entry<ServiceImage>(serviceImage).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
