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
            var currBooking = SessionHelper.GetObjectSession<BookingInformation>(HttpContext.Session, "BookingInformation");

            var selectedServicesId = SessionHelper.GetObjectSession<List<string>>(HttpContext.Session, "SelectedPetCareServices");

            var currUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");
            if (currUser != null)
            {
                currentUser = currUser;
                if (currBooking != null)
                {
                    Booking = new BookingCreateReqModel
                    {
                        BoardingType = currBooking.BoardingType,
                        StartDate = DateOnly.FromDateTime(currBooking.StartDate),
                        EndDate = DateOnly.FromDateTime(currBooking.EndDate),
                        Note = currBooking.Note,
                        AccommodationId = currBooking.AccommodationId,
                        PetId = currBooking.PetId
                    };

                    viewDataBooking(currentUser, Booking);

                    BookingServices = selectedServicesId;
                    return Page();
                }
                viewData(currUser);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");
            if (currUser != null)
            {
                currentUser = currUser;
            }

            if (String.IsNullOrEmpty(Booking.PetId))
            {
                viewData(currUser);
                TempData["ErrorPet"] = "Please add new Pet before booking";
                return Page();
            }

            if (!ModelState.IsValid)
            {
                viewData(currUser);
                return Page();
            }

            if (Booking.StartDate < DateOnly.FromDateTime(DateTime.Now))
            {
                viewData(currUser);
                TempData["ErrorDate"] = "Start date can not be in the past, please select again";
                return Page();
            }

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            switch (Booking.BoardingType)
            {
                case "Day care":
                    if (Booking.StartDate > Booking.EndDate || !Booking.StartDate.Equals(Booking.EndDate))
                    {
                        viewData(currUser);
                        TempData["ErrorDate"] = "For day care, start date must be the same as end date";
                        return Page();
                    }

                    if (Booking.StartDate.Equals(DateOnly.FromDateTime(DateTime.Now)))
                    {
                        if (Booking.StartDate.ToDateTime(TimeOnly.FromDateTime(DateTime.Now)) > DateTime.Today.AddHours(12))
                        {
                            viewData(currUser);
                            TempData["ErrorDate"] = "For day care, start date booking time must be before 12PM";
                            return Page();
                        }

                        start = DateTime.Now;
                    }else
                    {
                        start = Booking.StartDate.ToDateTime(new TimeOnly(8, 0));
                    }

                    end = Booking.EndDate.ToDateTime(new TimeOnly(21, 0));

                    if (BookingServices.Count <= 0)
                    {
                        viewData(currUser);
                        TempData["ErrorServices"] = "Please select at least one services";
                        return Page();
                    }
                    break;

                case "Overnight":
                    if (Booking.StartDate >= Booking.EndDate)
                    {
                        viewData(currUser);
                        TempData["ErrorDate"] = "For overnight, start date must be before end date";
                        return Page();
                    }

                    if (Booking.StartDate.Equals(DateOnly.FromDateTime(DateTime.Now)))
                    {
                        if (Booking.StartDate.ToDateTime(TimeOnly.FromDateTime(DateTime.Now)) > DateTime.Today.AddHours(21))
                        {
                            viewData(currUser);
                            TempData["ErrorDate"] = "For overnight, start date booking time must be before 21PM";
                            return Page();
                        }
                        start = DateTime.Now;
                    }else
                    {
                        start = Booking.StartDate.ToDateTime(new TimeOnly(8, 0));
                    }

                    end = Booking.EndDate.ToDateTime(new TimeOnly(21, 0));

                    if (BookingServices.Count <= 0)
                    {
                        viewData(currUser);
                        TempData["ErrorServices"] = "Please select at least one services";
                        return Page();
                    }
                    break;
            }

            var existingBookings = _bookingInformationService.GetBookingInformations()
                 .Where(x => x.PetId == Booking.PetId && x.Status.Equals(BookingStatusEnums.Pending.ToString()) &&
                 ((start <= x.StartDate && end >= x.StartDate) ||
                 (start <= x.EndDate && end >= x.EndDate) ||
                 (start >= x.StartDate && end <= x.EndDate)))
                 .ToList();

            List<Accommodation> bookedAccommdation = new List<Accommodation>();

            var accommodationBookings = _bookingInformationService.GetBookingInformations()
                 .Where(x => x.Status.Equals(BookingStatusEnums.Pending.ToString()) &&
                 ((start <= x.StartDate && end >= x.StartDate) ||
                 (start <= x.EndDate && end >= x.EndDate) ||
                 (start >= x.StartDate && end <= x.EndDate)))
                 .ToList();

            foreach (var item in accommodationBookings)
            {
                var accom = _accommodationService.GetAccommodationById(item.AccommodationId);

                if (accom != null)
                {
                    if (!bookedAccommdation.Contains(accom))
                    {
                        bookedAccommdation.Add(accom);
                    }
                }
            }

            Accommodation accommodation = _accommodationService.GetAccommodationById(Booking.AccommodationId);

            if (bookedAccommdation.Select(x => x.Id).ToList().Contains(Booking.AccommodationId))
            {
                viewData(currUser);
                TempData["ErrorAccommodation"] = $"Accommodation {accommodation.Name} is already booked within selected start date and end date";
                return Page();
            }

            if (existingBookings.Count > 0)
            {
                viewData(currUser);
                TempData["ErrorPetBooking"] = "Current pet is already have bookings";
                return Page();
            }


            SessionHelper.SetObjectSession(HttpContext.Session, "start", start);
            SessionHelper.SetObjectSession(HttpContext.Session, "end", end);

            SessionHelper.SetObjectSession(HttpContext.Session, "BookingInformation", Booking);
            SessionHelper.SetObjectSession(HttpContext.Session, "SelectedPetCareServices", BookingServices);

            return RedirectToPage("/User/Booking/BookingConfirmation");
        }

        private void viewData(BusinessObjects.Entities.User currentUser)
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
        }

        private void viewDataBooking(BusinessObjects.Entities.User currentUser, BookingCreateReqModel booking)
        {
            var petCareServices = _petCareService.GetPetCareServices().Where(x => x.Status.Equals(StatusEnums.Available.ToString())).ToList();
            PetCareServices = _mapper.Map<List<PetCareViewListResModel>>(petCareServices);

            var accommodations = _accommodationService.GetAccommodations();
            Accommodations = _mapper.Map<List<AccommodationViewListResModel>>(accommodations);

            var pets = _petService.GetListOfPets().Where(x => x.UserId.Equals(currentUser.Id)).ToList();
            Pets = _mapper.Map<List<PetViewListResModel>>(pets);

            ViewData["BoardingTypes"] = new SelectList(new List<string> { BoardingTypeEnums.DayCare.ToString(), BoardingTypeEnums.Overnight.ToString() }, booking.BoardingType);
            ViewData["Accommodations"] = new SelectList(Accommodations.Select(a => new { a.AccommodationId, Name = $"{a.Name} ({a.Type}) - {a.Price.ToString("#,##0")} VNĐ" }), "AccommodationId", "Name", booking.AccommodationId);
            ViewData["Pets"] = new SelectList(Pets.Select(p => new { p.PetId, Name = $"{p.Name} - {p.Breed} - {p.Age} {(p.Age > 1 ? "years old" : "year old")}" }), "PetId", "Name", booking.PetId);
        }
    }
}
