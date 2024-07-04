using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Enums.BookingStatusEnums;
using BusinessObjects.Models.BookingInformationModel.Request;
using BusinessObjects.Models.PetCareModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.AccommodationService;
using Services.Services.BookingInformationService;
using Services.Services.PetCareServices;
using Services.Services.PetService;
using Services.Services.ServicesBookingService;

namespace PetHotelApplicationRazorPage.Pages.User.Booking
{
    public class ConfirmationModel : AuthorizePageModel
    {
        private readonly IBookingInformationService _bookingInformation;
        private readonly IServiceBookingService _serviceBookingService;
        private readonly IAccommodationService _accommodationService;
        private readonly IPetService _petService;
        private readonly IPetCareService _petCareService;
        private readonly IMapper _mapper;

        public ConfirmationModel(IBookingInformationService bookingInformation,
            IServiceBookingService serviceBookingService,
            IAccommodationService accommodationService,
            IPetService petService,
            IPetCareService petCareService,
            IMapper mapper)
        {
            _bookingInformation = bookingInformation;
            _serviceBookingService = serviceBookingService;
            _accommodationService = accommodationService;
            _petService = petService;
            _petCareService = petCareService;
            _mapper = mapper;
        }

        [BindProperty]
        public BookingInformation Booking { get; set; }

        public List<PetCareViewListResModel> PetCareServices { get; set; }

        public string AccommodationName { get; set; }

        public string PetName { get; set; }

        public async Task <IActionResult> OnGet()
        {
            Booking = SessionHelper.GetObjectSession<BookingInformation>(HttpContext.Session, "BookingInformation");
            var selectedServiceId = SessionHelper.GetObjectSession<List<string>>(HttpContext.Session, "SelectedPetCareServices");

            if (Booking is null || selectedServiceId is null)
            {
                return RedirectToPage("/Index");
            }

            if (Booking != null)
            {
                var accommodation = _accommodationService.GetAccommodationById(Booking.AccommodationId);
                AccommodationName = $"{accommodation.Name} ({accommodation.Type}) - {accommodation.Price.ToString("#,##0")} VNĐ";

                var pet = _petService.GetPetById(Booking.PetId);
                PetName = $"{pet.PetName} - {pet.Breed} - {pet.Age} {(pet.Age > 1 ? "years old" : "year old")}";

                List<PetCareService> petCareServices = _petCareService.GetPetCareServicesByIds(selectedServiceId.ToList());

                PetCareServices = _mapper.Map<List<PetCareViewListResModel>>(petCareServices); 
            }
            return Page();
        }
    }
}
