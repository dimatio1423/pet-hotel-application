using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.AccommodationService
{
    public interface IAccommodationService
    {
        List<Accommodation> GetAccommodations();
        Accommodation GetAccommodationById(string id);
        void Add(Accommodation accommodation);
        void Delete(Accommodation accommodation);
        void Update(Accommodation accommodation);
    }
}
