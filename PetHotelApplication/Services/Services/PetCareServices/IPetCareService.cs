using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.PetCareServices
{
    public interface IPetCareService
    {
        List<PetCareService> GetPetCareServices();
        PetCareService GetPetCareServiceById(string id);
        void Add(PetCareService petCareService);
        void Delete(PetCareService petCareService);
        void Update(PetCareService petCareService);
    }
}
