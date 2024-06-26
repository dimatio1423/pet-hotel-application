using BusinessObjects.Entities;
using Repositories.Repositories.PetCareServiceRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.PetCareServices
{
    public class PetCareServices : IPetCareService
    {
        private readonly IPetCareServiceRepository _petCareServiceRepo;

        public PetCareServices(IPetCareServiceRepository petCareServiceRepo)
        {
            _petCareServiceRepo = petCareServiceRepo;
        }

        public string Add(PetCareService petCareService)
        {
            var result = _petCareServiceRepo.Add(petCareService);
            return result;
        }

        public void Delete(PetCareService petCareService)
        {
            _petCareServiceRepo.Delete(petCareService);
        }

        public PetCareService GetPetCareServiceById(string id)
        {
            return _petCareServiceRepo.GetPetCareServiceById(id);
        }

        public PetCareService GetPetCareServiceByType(string type)
        {
            return _petCareServiceRepo.GetPetCareServiceByType(type);
        }

        public List<PetCareService> GetPetCareServices()
        {
            return _petCareServiceRepo.GetPetCareServices();
        }

        public void Update(PetCareService petCareService)
        {
            _petCareServiceRepo.Update(petCareService);
        }
    }
}
