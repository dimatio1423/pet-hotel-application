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

namespace PetHotelApplicationRazorPage.Pages.User.Booking
{
    public class EditModel : AuthorizePageModel
    {
        private readonly IBookingInformationService _bookingInformationService;
        private readonly IAccommodationService _accommodationService;
        private readonly IPetCareService _petCareService;
        private readonly IPetService _petService;

        public EditModel(IBookingInformationService bookingInformationService,
            IAccommodationService accommodationService,
            IPetCareService petCareService,
            IPetService petService)
        {
            _bookingInformationService = bookingInformationService;
            _accommodationService = accommodationService;
            _petCareService = petCareService;
            _petService = petService;
        }

        [BindProperty]
        public UpdateBookingReqModel BookingUpdate { get; set; }
        [BindProperty]
        public List<AccommodationResModel> AvailableAccommodations { get; set; }
        [BindProperty]
        public List<PetCareServiceResModel> AvailableServices { get; set; }
        [BindProperty]
        public BookingDetailsResModel BookingDetails { get; set; }

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

            BookingDetails = new BookingDetailsResModel
            {
                Id = currentBooking.Id,
                BoardingType = currentBooking.BoardingType,
                StartDate = currentBooking.StartDate,
                EndDate = currentBooking.EndDate,
                Note = currentBooking.Note,
                Accommodation = new AccommodationResModel
                {
                    Type = currentBooking.Accommodation.Type
                },
                Pet = new PetResModel
                {
                    PetName = currentBooking.Pet.PetName
                },
                PetCareServices = currentBooking.ServiceBookings.Select(sb => new PetCareServiceResModel
                {
                    Type = sb.Service.Type,
                    Price = sb.Service.Price
                }).ToList()
            };

            BookingUpdate = new UpdateBookingReqModel
            {
                Id = BookingDetails.Id,
                Note = BookingDetails.Note,
                AccommodationType = BookingDetails.Accommodation.Type,
                ServiceTypes = BookingDetails.PetCareServices.Select(s => s.Type).ToList()
            };

            AvailableAccommodations = _accommodationService.GetAccommodations()
                .Select(a => new AccommodationResModel { Type = a.Type })
                .ToList();

            AvailableServices = _petCareService.GetPetCareServices()
                .Select(s => new PetCareServiceResModel { Type = s.Type, Price = s.Price })
                .ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingBooking = _bookingInformationService.GetBookingInformationById(BookingUpdate.Id);
            if (existingBooking == null)
            {
                return NotFound();
            }

            existingBooking.Note = BookingUpdate.Note;

            var accommodation = _accommodationService.GetAccommodationByType(BookingUpdate.AccommodationType);
            if (accommodation != null)
            {
                existingBooking.AccommodationId = accommodation.Id;
            }

            existingBooking.ServiceBookings.Clear();

            foreach (var serviceType in BookingUpdate.ServiceTypes)
            {
                var petCareService = _petCareService.GetPetCareServiceByType(serviceType);
                if (petCareService != null)
                {
                    existingBooking.ServiceBookings.Add(new ServiceBooking
                    {
                        ServiceId = petCareService.Id,
                        BookingId = existingBooking.Id
                    });
                }
            }

            _bookingInformationService.Update(existingBooking);

            return RedirectToPage("./Details", new { id = BookingUpdate.Id });
        }
    }
}
