using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.AccommodationRepo
{
    public interface IAccommodationRepository
    {
        List<Accommodation> GetAccommodations();
        Accommodation GetAccommodationById(string id);
        List<Accommodation> GetAccommodationsWithSearchSort(string searchValue, string sortOrder);
        void Add(Accommodation accommodation);
        void Delete(Accommodation accommodation);
        void Update(Accommodation accommodation);
    }
}
