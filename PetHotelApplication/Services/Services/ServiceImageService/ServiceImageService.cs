using BusinessObjects.Entities;
using Repositories.Repositories.ServiceImageRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.ServiceImageService
{
    public class ServiceImageService : IServiceImageService
    {
        private readonly IServiceImageRepository _serviceImageRepository;

        public ServiceImageService(IServiceImageRepository serviceImageRepository)
        {
            _serviceImageRepository = serviceImageRepository;
        }
        public void Add(ServiceImage serviceImage)
        {
            _serviceImageRepository.Add(serviceImage);
        }

        public void Delete(ServiceImage serviceImage)
        {
            _serviceImageRepository.Delete(serviceImage);
        }

        public List<ServiceImage> GetServiceImagesByServiceId(string Id)
        {
            return _serviceImageRepository.GetServiceImagesByServiceId(Id);
        }

        public void Update(ServiceImage serviceImage)
        {
            _serviceImageRepository.Update(serviceImage);
        }
    }
}
