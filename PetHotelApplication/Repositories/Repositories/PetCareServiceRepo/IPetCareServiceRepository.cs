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
        string Add(PetCareService petCareService);
        void Delete(PetCareService petCareService);
        void Update(PetCareService petCareService);
        PetCareService GetPetCareServiceByType(string type);
    }
}
