using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.PetCareServiceRepo
{
    public interface IPetCareServiceRepository
    {
        List<PetCareService> GetPetCareServices();
        PetCareService GetPetCareServiceById(string id);
        void Add(PetCareService petCareService);
        void Delete(PetCareService petCareService);
        void Update(PetCareService petCareService);
    }
}
