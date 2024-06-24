using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.ServiceImageRepo
{
    public interface IServiceImageRepository
    {
        List<ServiceImage> GetServiceImagesByServiceId(string Id);
        void Add(ServiceImage serviceImage);
        void Delete(ServiceImage serviceImage);
        void Update(ServiceImage serviceImage);
    }
}
