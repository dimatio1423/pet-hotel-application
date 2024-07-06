using AutoMapper;
using BusinessObjects.Models.BookingInformationModel.Request;
using BusinessObjects.Models.BookingInformationModel.Response;
using Services.Services.PetService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Resolver.BookingInformationResolver
{
    public class BookingPetResolver : IValueResolver<BookingCreateReqModel, BookingInformationViewResModel, string>
    {
        private readonly IPetService _petService;

        public BookingPetResolver(IPetService petService)
        {
            _petService = petService;
        }
        public string Resolve(BookingCreateReqModel source, BookingInformationViewResModel destination, string destMember, ResolutionContext context)
        {
            var pet = _petService.GetPetById(source.PetId);

            return $"{pet.PetName} - {pet.Breed} - {pet.Age} {(pet.Age > 1 ? "years old" : "year old")}";
        } 
    }
}
