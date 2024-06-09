using BusinessObjects.Entities;
using Repositories.Repositories.ServiceBookingRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.ServicesBookingService
{
    public class ServiceBookingService : IServiceBookingService
    {
        private readonly IServiceBookingRepository _serviceBookingRepo;

        public ServiceBookingService(IServiceBookingRepository serviceBookingRepo)
        {
            _serviceBookingRepo = serviceBookingRepo;
        }
        public void Add(ServiceBooking serviceBooking)
        {
            _serviceBookingRepo.Add(serviceBooking);
        }

        public void Delete(ServiceBooking serviceBooking)
        {
            _serviceBookingRepo.Delete(serviceBooking);
        }

        public List<ServiceBooking> GetServiceBookingsByBookingId(string Id)
        {
            return _serviceBookingRepo.GetServiceBookingsByBookingId(Id);
        }

        public void Update(ServiceBooking serviceBooking)
        {
            _serviceBookingRepo.Update(serviceBooking);
        }
    }
}
