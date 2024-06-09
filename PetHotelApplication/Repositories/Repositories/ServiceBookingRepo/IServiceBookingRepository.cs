using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.ServiceBookingRepo
{
    public interface IServiceBookingRepository
    {
        List<ServiceBooking> GetServiceBookingsByBookingId(string Id);
        void Add(ServiceBooking serviceBooking);
        void Delete(ServiceBooking serviceBooking);
        void Update(ServiceBooking serviceBooking);
    }
}
