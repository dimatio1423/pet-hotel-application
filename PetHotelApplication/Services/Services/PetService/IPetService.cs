using BusinessObjects.Entities;
using BusinessObjects.Models.PetModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.PetService
{
    public interface IPetService
    {
        List<PetResModel> GetPets();
        Pet GetPetById(string id);
        void Add(Pet pet);
        void Delete(Pet pet);
        void Update(Pet pet);
        List<PetResModel> GetUserPets(string userId, string petName);
        PetResModel GetPetDetailsById(string id);
        public void Update(PetResModel pet);
        public List<PetSpeciesResModel> GetPetSpecies();
        List<Pet> GetListOfPets();
    }
}
