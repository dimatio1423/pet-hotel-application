using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Enums.BookingStatusEnums;
using BusinessObjects.Enums.PaymenStatusEnums;
using BusinessObjects.Models.BookingInformationModel.Request;
using BusinessObjects.Models.PetCareModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.AccommodationService;
using Services.Services.BookingInformationService;
using Services.Services.PaymentRecordService;
using Services.Services.PetCareServices;
using Services.Services.PetService;
using Services.Services.ServicesBookingService;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;

namespace PetHotelApplicationRazorPage.Pages.User.Booking
{
    public class ConfirmationModel : AuthorizePageModel
    {
        private readonly IBookingInformationService _bookingInformationService;
        private readonly IServiceBookingService _serviceBookingService;
        private readonly IAccommodationService _accommodationService;
        private readonly IPetService _petService;
        private readonly IPetCareService _petCareService;
        private readonly IPaymentRecordService _paymentRecordService;
        private readonly IMapper _mapper;

        public ConfirmationModel(IBookingInformationService bookingInformationService,
            IServiceBookingService serviceBookingService,
            IAccommodationService accommodationService,
            IPetService petService,
            IPetCareService petCareService,
            IPaymentRecordService paymentRecordService,
            IMapper mapper)
        {
            _bookingInformationService = bookingInformationService;
            _serviceBookingService = serviceBookingService;
            _accommodationService = accommodationService;
            _petService = petService;
            _petCareService = petCareService;
            _paymentRecordService = paymentRecordService;
            _mapper = mapper;
        }
        [BindProperty]
        public decimal TotalPrice { get; set; }

        [BindProperty]
        //[Required(ErrorMessage = "Please select a payment method.")]
        public string SelectedPaymentMethod { get; set; }

        [BindProperty]
        public BookingCreateReqModel Booking { get; set; }

        [BindProperty]
        public List<PetCareViewListResModel> PetCareServices { get; set; }

        public string AccommodationName { get; set; }

        public string PetName { get; set; }

        public async Task <IActionResult> OnGet()
        {
            return await loadData();
        }

        public async Task <IActionResult> OnPostConfirm()
        {
            //BookingInformation bookingInformation = SessionHelper.GetObjectSession<BookingInformation>(HttpContext.Session, "BookingInformation");

            //var selectedServiceId = SessionHelper.GetObjectSession<List<string>>(HttpContext.Session, "SelectedPetCareServices");

            if (String.IsNullOrEmpty(SelectedPaymentMethod))
            {
                TempData["ErrorPaymentMethod"] = "Please select a payment method.";

                return await loadData();

                //return Page();
            }

            if (SelectedPaymentMethod == "Cash")
            {
                var currUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");

                var bookingInformation = SessionHelper.GetObjectSession<BookingCreateReqModel>(HttpContext.Session, "BookingInformation");

                var selectedServiceId = SessionHelper.GetObjectSession<List<string>>(HttpContext.Session, "SelectedPetCareServices");

                var start = SessionHelper.GetObjectSession<DateTime>(HttpContext.Session, "start");

                var end = SessionHelper.GetObjectSession<DateTime>(HttpContext.Session, "end");

                if (bookingInformation != null)
                {
                    BookingInformation newBookingInformation = new BookingInformation
                    {
                        Id = Guid.NewGuid().ToString(),
                        BoardingType = bookingInformation.BoardingType,
                        StartDate = start,
                        EndDate = end,
                        Note = bookingInformation.Note,
                        Status = BookingStatusEnums.Pending.ToString(),
                        UserId = currUser.Id,
                        AccommodationId = bookingInformation.AccommodationId,
                        PetId = bookingInformation.PetId
                    };

                    _bookingInformationService.Add(newBookingInformation);

                    List<PetCareService> petCareServices = _petCareService.GetPetCareServicesByIds(selectedServiceId.ToList());

                    foreach (var item in petCareServices)
                    {
                        ServiceBooking serviceBooking = new ServiceBooking
                        {
                            Id = Guid.NewGuid().ToString(),
                            BookingId = newBookingInformation.Id,
                            ServiceId = item.Id
                        };

                        _serviceBookingService.Add(serviceBooking);
                    }

                    createPayment(newBookingInformation);
                }
                return RedirectToPage("/Index");
            }
            else if (SelectedPaymentMethod == "TransferCash")
            {
                return RedirectToPage("Payment");
            }

            return Page();
        }

        public IActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("BookingInformation");
            HttpContext.Session.Remove("SelectedPetCareServices");
            HttpContext.Session.Remove("start");
            HttpContext.Session.Remove("end");

            return RedirectToPage("/User/Booking/Create");
        }

        private async Task <IActionResult> loadData()
        {
            Booking = SessionHelper.GetObjectSession<BookingCreateReqModel>(HttpContext.Session, "BookingInformation");

            var selectedServiceId = SessionHelper.GetObjectSession<List<string>>(HttpContext.Session, "SelectedPetCareServices");

            List<PetCareService> petCareServices = _petCareService.GetPetCareServicesByIds(selectedServiceId.ToList());

            var start = SessionHelper.GetObjectSession<DateTime>(HttpContext.Session, "start");

            var end = SessionHelper.GetObjectSession<DateTime>(HttpContext.Session, "end");

            decimal totalPrice = 0;

            decimal totalServicePrice = 0;

            int days;

            if (Booking is null || selectedServiceId is null)
            {
                return RedirectToPage("/Index");
            }

            if (Booking != null)
            {
                if (end.Date.Equals(start.Date))
                {
                    days = 1;
                }
                else
                {
                    TimeSpan totalDay = end.Date.Subtract(start.Date);
                    days = totalDay.Days;
                }

                foreach (var service in petCareServices)
                {
                    var currentService = _petCareService.GetPetCareServiceById(service.Id);
                    totalServicePrice += currentService.Price;
                }

                var currAccommodation = _accommodationService.GetAccommodationById(Booking.AccommodationId);

                totalPrice = (currAccommodation.Price + totalServicePrice) * days;

                var accommodation = _accommodationService.GetAccommodationById(Booking.AccommodationId);
                AccommodationName = $"{accommodation.Name} ({accommodation.Type}) - {accommodation.Price.ToString("#,##0")} VNĐ";

                var pet = _petService.GetPetById(Booking.PetId);
                PetName = $"{pet.PetName} - {pet.Breed} - {pet.Age} {(pet.Age > 1 ? "years old" : "year old")}";

                PetCareServices = _mapper.Map<List<PetCareViewListResModel>>(petCareServices);

                TotalPrice = totalPrice;
            }
            return Page();
        }

        private void createPayment(BookingInformation bookingInformation)
        {
            var currBooking = _bookingInformationService.GetBookingInformationById(bookingInformation.Id);

            decimal totalPrice = 0;

            decimal totalServicePrice = 0;

            int days;

            if (currBooking != null)
            {
                if (currBooking.EndDate.Date.Equals(currBooking.StartDate.Date))
                {
                    days = 1;
                }
                else
                {
                    TimeSpan totalDay = currBooking.EndDate.Date.Subtract(currBooking.StartDate.Date);
                    days = totalDay.Days;

                }

                foreach (var service in currBooking.ServiceBookings)
                {
                    var currentService = _petCareService.GetPetCareServiceById(service.ServiceId);
                    totalServicePrice += currentService.Price;
                }

                var accommodation = _accommodationService.GetAccommodationById(currBooking.AccommodationId);

                totalPrice = (accommodation.Price + totalServicePrice) * days;

                PaymentRecord paymentRecord = new PaymentRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    Price = totalPrice.ToString(),
                    Date = DateTime.Now,
                    Method = "Cash",
                    Status = PaymentStatusEnums.Unpaid.ToString(),
                    UserId = currBooking.UserId,
                    BookingId = currBooking.Id
                };

                _paymentRecordService.Add(paymentRecord);

                HttpContext.Session.Remove("BookingInformation");

                HttpContext.Session.Remove("SelectedPetCareServices");
            }
        }
    }
}
