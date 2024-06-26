using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Models.PetCareModel.Response;
using BusinessObjects.Models.PetModel.Request;
using BusinessObjects.Models.PetModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PetCareService, PetCareResModel>();

            CreateMap<Pet, PetResModel>()
                .ReverseMap();
            CreateMap<BookingInformation, BookingInformationResModel>()
                .ForMember(dest => dest.PetCareServices, opt => opt.MapFrom(src => string.Join(", ", src.ServiceBookings.Select(sb => sb.Service.Type))));
            CreateMap<Accommodation, AccommodationResModel>();
            CreateMap<PaymentRecord, PaymentRecordResModel>();

            CreateMap<PetCreateReqModel, Pet>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
        }
    }
}
