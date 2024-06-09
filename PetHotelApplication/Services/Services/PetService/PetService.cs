using BusinessObjects.Entities;
using Repositories.Repositories.PetRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.PetService
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepo;

        public PetService(IPetRepository petRepo)
        {
            _petRepo = petRepo;
        }
        public void Add(Pet pet)
        {
            _petRepo.Add(pet);
        }

        public void Delete(Pet pet)
        {
            _petRepo.Delete(pet);
        }

        public Pet GetPetById(string id)
        {
            return _petRepo.GetPetById(id);
        }

        public List<Pet> GetPets()
        {
            return _petRepo.GetPets();
        }

        public void Update(Pet pet)
        {
            _petRepo.Update(pet);
        }
    }
}
