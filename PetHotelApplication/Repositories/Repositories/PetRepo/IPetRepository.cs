using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.PetRepo
{
    public interface IPetRepository
    {
        List<Pet> GetPets();
        Pet GetPetById(string id);
        Pet? GetPetDetailsById(string id);
        void Add(Pet pet);
        void Delete(Pet pet);
        void Update(Pet pet);
        public List<Pet> GetUserPets(string userId, string petName);
    }
}
