using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Models.AccommodationModel.Request;
using BusinessObjects.Models.Accommodation.Response;
using BusinessObjects.Models.PetCareModel.Response;
using BusinessObjects.Models.PetModel.Request;
using BusinessObjects.Models.PetModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models.PaymentRecordModel.Response;
using BusinessObjects.Models.BookingInformationModel.Response;
using System.Collections;
using System.Linq.Expressions;

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

            CreateMap<BookingInformation, BookingContinuePaymentResModel>()
                .ForMember(dest => dest.PetCareServices, opt => opt.MapFrom(src => src.ServiceBookings.Select(sb => sb.Service)))
                .ForMember(dest => dest.Pet, opt => opt.MapFrom(src =>
                    $"{src.Pet.PetName} - {src.Pet.Breed} - {src.Pet.Age} {(src.Pet.Age > 1 ? "years old" : "year old")}"))
                .ForMember(dest => dest.Accommodation, opt => opt.MapFrom(src =>
                    $"{src.Accommodation.Name} ({src.Accommodation.Type}) - {src.Accommodation.Price.ToString("#,##0")} VNĐ"))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src =>                
                    Math.Max(1, (int)src.EndDate.Subtract(src.StartDate).TotalDays) * (src.ServiceBookings.Sum(sb => sb.Service.Price) + src.Accommodation.Price)
                ));

            CreateMap<Accommodation, AccommodationResModel>();

            CreateMap<PaymentRecord, PaymentRecordResModel>();

            CreateMap<PetCreateReqModel, Pet>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
            CreateMap<AccommodationCreateReqModel, Accommodation>();
            CreateMap<AccommodationUpdateReqModel, Accommodation>().ReverseMap();

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
