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
        List<Accommodation> GetAccommodationsWithSearchSort(string searchValue, string sortOrder);
        Accommodation GetAccommodationById(string id);
        Accommodation GetAccommodationByType(string type);
        void Add(Accommodation accommodation);
        void Delete(Accommodation accommodation);
        void Update(Accommodation accommodation);
    }
}
