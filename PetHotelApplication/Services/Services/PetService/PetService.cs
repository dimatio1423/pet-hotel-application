using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Enums.StatusEnums;
using BusinessObjects.Models.PetModel.Response;
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
        private readonly IMapper _mapper;

        public PetService(IPetRepository petRepo, IMapper mapper)
        {
            _petRepo = petRepo;
            _mapper = mapper;
        }
        public void Add(Pet pet)
        {
            pet.Status = StatusEnums.Active.ToString();
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

        public List<PetResModel> GetPets()
        {
            var list = _mapper.Map<List<PetResModel>>(_petRepo.GetPets());
            return list;
        }

        public void Update(Pet pet)
        {
            _petRepo.Update(pet);
        }

        public List<PetResModel> GetUserPets(string userId, string petName)
        {
            var list = _mapper.Map<List<PetResModel>>(_petRepo.GetUserPets(userId, petName));
            return list;
        }

        public PetResModel GetPetDetailsById(string id)
        {
            var pet = _mapper.Map<PetResModel>(_petRepo.GetPetDetailsById(id));
            return pet;
        }

        public void Update(PetResModel pet)
        {
            Pet currentPet = GetPetById(pet.Id);
            _mapper.Map(pet, currentPet);

            //updatePet.Status = currentPet.Status;
            //updatePet.UserId = currentPet.UserId;

            _petRepo.Update(currentPet);
        }

        public List<PetSpeciesResModel> GetPetSpecies()
        {
            return new List<PetSpeciesResModel>
            {
                new PetSpeciesResModel
                {
                    Name = "Dog"
                },
                new PetSpeciesResModel
                {
                    Name = "Cat"
                }
            };
        }

        public List<Pet> GetListOfPets()
        {
            return _petRepo.GetPets();
        }
    }
}
