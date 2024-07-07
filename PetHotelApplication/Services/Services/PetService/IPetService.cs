using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.PetService
{
    public interface IPetService
    {
        List<Pet> GetPets();
        Pet GetPetById(string id);
        void Add(Pet pet);
        void Delete(Pet pet);
        void Update(Pet pet);
    }
}
