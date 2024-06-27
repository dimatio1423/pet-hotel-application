using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Models.Accommodation.Response;
using BusinessObjects.Models.PetCareModel.Response;
using BusinessObjects.Models.PetModel.Request;
using BusinessObjects.Models.PetModel.Response;
using BusinessObjects.Models.BookingModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models.PaymentRecordModel.Response;

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

            CreateMap<PetCareService, PetCareViewListResModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Accommodation, AccommodationViewListResModel>()
                .ForMember(dest => dest.AccommodationId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Pet, PetViewListResModel>()
                .ForMember(dest => dest.PetId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PetName))
                .ReverseMap();
        }
    }
}
