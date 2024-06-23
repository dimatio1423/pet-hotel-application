using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.ServiceImageService
{
    public interface IServiceImageService
    {
        List<ServiceImage> GetServiceImagesByServiceId(string Id);
        void Add(ServiceImage serviceImage);
        void Delete(ServiceImage serviceImage);
        void Update(ServiceImage serviceImage);
    }
}
