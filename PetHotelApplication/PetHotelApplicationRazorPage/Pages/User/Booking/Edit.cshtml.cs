using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models.BookingHistoryModel.Response;
using Services.Services.AccommodationService;
using Services.Services.BookingInformationService;
using Services.Services.PetCareServices;
using Services.Services.PetService;
using BusinessObjects.Entities;
using BusinessObjects.Models.BookingHistoryModel.Request;
using BusinessObjects.Models.BookingInformationModel.Response;
using AutoMapper;
using BusinessObjects.Enums.StatusEnums;
using BusinessObjects.Models.Accommodation.Response;
using BusinessObjects.Models.PetCareModel.Response;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Enums.BoardingTypeEnums;
using BusinessObjects.Enums.BookingStatusEnums;
using Services.Services.ServicesBookingService;

namespace PetHotelApplicationRazorPage.Pages.User.Booking
{
    public class EditModel : AuthorizePageModel
    {
        private readonly IBookingInformationService _bookingInformationService;
        private readonly IAccommodationService _accommodationService;
        private readonly IPetCareService _petCareService;
        private readonly IPetService _petService;
        private readonly IServiceBookingService _serviceBookingService;
        private readonly IMapper _mapper;

        public EditModel(IBookingInformationService bookingInformationService,
            IAccommodationService accommodationService,
            IPetCareService petCareService,
            IServiceBookingService serviceBookingService,
            IPetService petService, IMapper mapper)
        {
            _bookingInformationService = bookingInformationService;
            _accommodationService = accommodationService;
            _petCareService = petCareService;
            _petService = petService;
            _serviceBookingService = serviceBookingService;
            _mapper = mapper;
        }

        [BindProperty]
        public UpdateBookingReqModel BookingUpdate { get; set; }

        [BindProperty]
        public BookingContinuePaymentResModel BookingDetails { get; set; }
        public IList<PetCareViewListResModel> PetCareServices { get; set; } = new List<PetCareViewListResModel>();
        public IList<AccommodationViewListResModel> Accommodations { get; set; } = new List<AccommodationViewListResModel>();

        [BindProperty]
        [Required(ErrorMessage = "Please select at least one service.")]
        public IList<string> BookingServices { get; set; } = new List<string>();

        public IActionResult OnGet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentBooking = _bookingInformationService.GetBookingInformationById(id);

            if (currentBooking == null)
            {
                return NotFound();
            }

            viewDataBooking(currentBooking);

            return Page();
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var existingBooking = _bookingInformationService.GetBookingInformationById(BookingUpdate.Id);
            if (existingBooking == null)
            {
                return NotFound();
            }

            if (BookingServices.Count <= 0)
            {
                TempData["ErrorServices"] = "Please select at least one service for booking";
                viewDataBooking(existingBooking);
                return Page();
            }

            existingBooking.Note = BookingUpdate.Note;

            var accommodation = _accommodationService.GetAccommodationById(BookingUpdate.AccommodationId);
            if (accommodation != null)
            {
                existingBooking.AccommodationId = accommodation.Id;
            }

            foreach(var item in existingBooking.ServiceBookings)
            {
                _serviceBookingService.Delete(item);
            }

            foreach (var service in BookingServices)
            {
                var petCareService = _petCareService.GetPetCareServiceById(service);
                if (petCareService != null)
                {
                    _serviceBookingService.Add(new ServiceBooking
                    {
                        Id = Guid.NewGuid().ToString(),
                        ServiceId = petCareService.Id,
                        BookingId = existingBooking.Id
                    });
                }
            }

            _bookingInformationService.Update(existingBooking);

            return RedirectToPage("./Details", new { id = BookingUpdate.Id });
        }

        private void viewDataBooking(BookingInformation booking)
        {
            BookingDetails = _mapper.Map<BookingContinuePaymentResModel>(booking);

            BookingUpdate = new UpdateBookingReqModel
            {
                Id = BookingDetails.Id,
                Note = BookingDetails.Note,
                AccommodationId = booking.AccommodationId,
            };

            var petCareServices = _petCareService.GetPetCareServices().Where(x => x.Status.Equals(StatusEnums.Available.ToString())).ToList();
            PetCareServices = _mapper.Map<List<PetCareViewListResModel>>(petCareServices);

            var accommodations = _accommodationService.GetAccommodations().Where(x => x.Status.Equals(StatusEnums.Available.ToString())).ToList();
            Accommodations = _mapper.Map<List<AccommodationViewListResModel>>(accommodations);

            ViewData["BoardingTypes"] = new SelectList(new List<string> { BoardingTypeEnums.DayCare.ToString(), BoardingTypeEnums.Overnight.ToString() }, booking.BoardingType);
            ViewData["Accommodations"] = new SelectList(Accommodations.Select(a => new { a.AccommodationId, Name = $"{a.Name} ({a.Type}) - {a.Price.ToString("#,##0")} VNĐ" }), "AccommodationId", "Name", booking.AccommodationId);
        }
    }
}
