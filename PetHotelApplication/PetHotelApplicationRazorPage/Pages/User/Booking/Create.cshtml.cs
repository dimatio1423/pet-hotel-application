using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Enums.BoardingTypeEnums;
using BusinessObjects.Enums.BookingStatusEnums;
using BusinessObjects.Enums.StatusEnums;
using BusinessObjects.Models.Accommodation.Response;
using BusinessObjects.Models.BookingInformationModel.Request;
using BusinessObjects.Models.PetCareModel.Response;
using BusinessObjects.Models.PetModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Services.AccommodationService;
using Services.Services.BookingInformationService;
using Services.Services.PetCareServices;
using Services.Services.PetService;
using Services.Services.ServicesBookingService;
using System.ComponentModel.DataAnnotations;

namespace PetHotelApplicationRazorPage.Pages.User.Booking
{
    public class CreateModel : AuthorizePageModel
    {
        private readonly IBookingInformationService _bookingInformationService;
        private readonly IAccommodationService _accommodationService;
        private readonly IPetCareService _petCareService;
        private readonly IPetService _petService;
        private readonly IServiceBookingService _serviceBookingService;
        private readonly IMapper _mapper;

        public CreateModel(IBookingInformationService bookingInformationService, 
            IAccommodationService accommodationService,
            IPetCareService petCareService,
            IPetService petService,
            IServiceBookingService serviceBookingService,
            IMapper mapper)
        {
            _bookingInformationService = bookingInformationService;
            _accommodationService = accommodationService;
            _petCareService = petCareService;
            _petService = petService;
            _serviceBookingService = serviceBookingService;
            _mapper = mapper;
        }

        public BusinessObjects.Entities.User currentUser { get; set; }

        [BindProperty]
        public BookingCreateReqModel Booking { get; set; } = default!;
        public IList<PetCareViewListResModel> PetCareServices { get; set; } = new List<PetCareViewListResModel>();
        public IList<AccommodationViewListResModel> Accommodations { get; set; } = new List<AccommodationViewListResModel>();
        public IList<PetViewListResModel> Pets { get; set; } = new List<PetViewListResModel>();

        [BindProperty]
        [Required(ErrorMessage = "Please select at least one service.")]
        public IList<string> BookingServices { get; set; } = new List<string>();
        public async Task<IActionResult> OnGet()
        {
            var currUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");
            if (currUser != null)
            {
                currentUser = currUser;
            }

            var petCareServices = _petCareService.GetPetCareServices().Where(x => x.Status.Equals(StatusEnums.Available.ToString())).ToList();
            PetCareServices = _mapper.Map<List<PetCareViewListResModel>>(petCareServices);

            var accommodations = _accommodationService.GetAccommodations();
            Accommodations = _mapper.Map<List<AccommodationViewListResModel>>(accommodations);

            var pets = _petService.GetListOfPets().Where(x => x.UserId.Equals(currentUser.Id)).ToList();
            Pets = _mapper.Map<List<PetViewListResModel>>(pets);

            ViewData["BoardingTypes"] = new SelectList(new List<string> { BoardingTypeEnums.DayCare.ToString(), BoardingTypeEnums.Overnight.ToString() });
            ViewData["Accommodations"] = new SelectList(Accommodations.Select(a => new { a.AccommodationId, Name = $"{a.Name} ({a.Type}) - {a.Price.ToString("#,##0")} VNĐ" }), "AccommodationId", "Name");
            ViewData["Pets"] = new SelectList(Pets.Select(p => new {p.PetId, Name = $"{p.Name} - {p.Breed} - {p.Age} {(p.Age > 1 ? "years old" : "year old")}"}), "PetId", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");
            if (currUser != null)
            {
                currentUser = currUser;
            }

            if (!ModelState.IsValid)
            {
                var petCareServices = _petCareService.GetPetCareServices().Where(x => x.Status.Equals(StatusEnums.Available.ToString())).ToList();
                PetCareServices = _mapper.Map<List<PetCareViewListResModel>>(petCareServices);

                var accommodations = _accommodationService.GetAccommodations();
                Accommodations = _mapper.Map<List<AccommodationViewListResModel>>(accommodations);

                var pets = _petService.GetListOfPets().Where(x => x.UserId.Equals(currentUser.Id)).ToList();
                Pets = _mapper.Map<List<PetViewListResModel>>(pets);

                ViewData["BoardingTypes"] = new SelectList(new List<string> { BoardingTypeEnums.DayCare.ToString(), BoardingTypeEnums.Overnight.ToString() });
                ViewData["Accommodations"] = new SelectList(Accommodations.Select(a => new { a.AccommodationId, Name = $"{a.Name} ({a.Type}) - {a.Price.ToString("#,##0")} VNĐ" }), "AccommodationId", "Name");
                ViewData["Pets"] = new SelectList(Pets.Select(p => new { p.PetId, Name = $"{p.Name} - {p.Breed} - {p.Age} {(p.Age > 1 ? "years old" : "year old")}" }), "PetId", "Name");

                return Page();
            }

            if (Booking.StartDate > Booking.EndDate)
            {
                var petCareServices = _petCareService.GetPetCareServices().Where(x => x.Status.Equals(StatusEnums.Available.ToString())).ToList();
                PetCareServices = _mapper.Map<List<PetCareViewListResModel>>(petCareServices);

                var accommodations = _accommodationService.GetAccommodations();
                Accommodations = _mapper.Map<List<AccommodationViewListResModel>>(accommodations);

                var pets = _petService.GetListOfPets().Where(x => x.UserId.Equals(currentUser.Id)).ToList();
                Pets = _mapper.Map<List<PetViewListResModel>>(pets);

                ViewData["BoardingTypes"] = new SelectList(new List<string> { BoardingTypeEnums.DayCare.ToString(), BoardingTypeEnums.Overnight.ToString() });
                ViewData["Accommodations"] = new SelectList(Accommodations.Select(a => new { a.AccommodationId, Name = $"{a.Name} ({a.Type}) - {a.Price.ToString("#,##0")} VNĐ" }), "AccommodationId", "Name");
                ViewData["Pets"] = new SelectList(Pets.Select(p => new { p.PetId, Name = $"{p.Name} - {p.Breed} - {p.Age} {(p.Age > 1 ? "years old" : "year old")}" }), "PetId", "Name");
                TempData["ErrorDate"] = "Please select start date before end date";
                return Page();
            }

            if (BookingServices.Count <= 0)
            {
                var petCareServices = _petCareService.GetPetCareServices().Where(x => x.Status.Equals(StatusEnums.Available.ToString())).ToList();
                PetCareServices = _mapper.Map<List<PetCareViewListResModel>>(petCareServices);

                var accommodations = _accommodationService.GetAccommodations();
                Accommodations = _mapper.Map<List<AccommodationViewListResModel>>(accommodations);

                var pets = _petService.GetListOfPets().Where(x => x.UserId.Equals(currentUser.Id)).ToList();
                Pets = _mapper.Map<List<PetViewListResModel>>(pets);

                ViewData["BoardingTypes"] = new SelectList(new List<string> { BoardingTypeEnums.DayCare.ToString(), BoardingTypeEnums.Overnight.ToString() });
                ViewData["Accommodations"] = new SelectList(Accommodations.Select(a => new { a.AccommodationId, Name = $"{a.Name} ({a.Type}) - {a.Price.ToString("#,##0")} VNĐ" }), "AccommodationId", "Name");
                ViewData["Pets"] = new SelectList(Pets.Select(p => new { p.PetId, Name = $"{p.Name} - {p.Breed} - {p.Age} {(p.Age > 1 ? "years old" : "year old")}" }), "PetId", "Name");
                TempData["ErrorServices"] = "Please select at least one services";
                return Page();
            }

            BookingInformation bookingInformation = new BookingInformation
            {
                Id = Guid.NewGuid().ToString(),
                BoardingType = Booking.BoardingType,
                StartDate = Booking.StartDate,
                EndDate = Booking.EndDate,
                Note = Booking.Note,
                Status = BookingStatusEnums.Pending.ToString(),
                UserId = currentUser.Id,
                AccommodationId = Booking.AccommodationId,
                PetId = Booking.PetId
            };

            _bookingInformationService.Add(bookingInformation);

            List<PetCareService> petCares = _petCareService.GetPetCareServicesByIds(BookingServices.ToList());

            foreach (var item in petCares)
            {
                ServiceBooking serviceBooking = new ServiceBooking
                {
                    Id = Guid.NewGuid().ToString(),
                    BookingId = bookingInformation.Id,
                    ServiceId = item.Id
                };

                _serviceBookingService.Add(serviceBooking);
            }

            return RedirectToPage("/User/Payment/PaymentMethod");
        }
    }
}
