using AutoMapper;
using BusinessObjects.Models.BookingInformationModel.Request;
using BusinessObjects.Models.BookingInformationModel.Response;
using Services.Services.AccommodationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Resolver.BookingInformationResolver
{
    public class BookingAccommodationResolver : IValueResolver<BookingCreateReqModel, BookingInformationViewResModel, string>
    {
        private readonly IAccommodationService _accommodationService;

        public BookingAccommodationResolver(IAccommodationService accommodationService)
        {
            _accommodationService = accommodationService;
        }
        public string Resolve(BookingCreateReqModel source, BookingInformationViewResModel destination, string destMember, ResolutionContext context)
        {
            var accommodation = _accommodationService.GetAccommodationById(source.AccommodationId);

            return $"{accommodation.Name} ({accommodation.Type}) - {accommodation.Price.ToString("#,##0")} VNĐ";
        }
    }
}
