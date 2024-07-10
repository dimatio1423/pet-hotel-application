using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Enums.BookingStatusEnums;
using BusinessObjects.Enums.PaymenStatusEnums;
using BusinessObjects.Models.BookingInformationModel.Request;
using BusinessObjects.Models.BookingInformationModel.Response;
using BusinessObjects.Models.PetCareModel.Response;
using BusinessObjects.Models.VnPaymentModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.AccommodationService;
using Services.Services.BookingInformationService;
using Services.Services.PaymentRecordService;
using Services.Services.PetCareServices;
using Services.Services.PetService;
using Services.Services.ServicesBookingService;
using Services.Services.VnPaymentServices;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
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
        private readonly IVnPayService _vnPayService;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public ConfirmationModel(IBookingInformationService bookingInformationService,
            IServiceBookingService serviceBookingService,
            IAccommodationService accommodationService,
            IPetService petService,
            IPetCareService petCareService,
            IPaymentRecordService paymentRecordService,
            IVnPayService vnPayService,
            IMapper mapper,
            IHttpClientFactory httpClientFactory)
        {
            _bookingInformationService = bookingInformationService;
            _serviceBookingService = serviceBookingService;
            _accommodationService = accommodationService;
            _petService = petService;
            _petCareService = petCareService;
            _paymentRecordService = paymentRecordService;
            _vnPayService = vnPayService;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }
        [BindProperty]
        public decimal TotalPrice { get; set; }

        [BindProperty]
        //[Required(ErrorMessage = "Please select a payment method.")]
        public string SelectedPaymentMethod { get; set; }

        [BindProperty]
        public BookingInformationViewResModel Booking { get; set; }

        [BindProperty]
        public List<PetCareViewListResModel> PetCareServices { get; set; }

        public string AccommodationName { get; set; }

        public string PetName { get; set; }

        public async Task <IActionResult> OnGet()
        {
            return await viewData();
        }

        public async Task <IActionResult> OnPostConfirm()
        {
            //BookingInformation bookingInformation = SessionHelper.GetObjectSession<BookingInformation>(HttpContext.Session, "BookingInformation");

            //var selectedServiceId = SessionHelper.GetObjectSession<List<string>>(HttpContext.Session, "SelectedPetCareServices");

            if (String.IsNullOrEmpty(SelectedPaymentMethod))
            {
                TempData["ErrorPaymentMethod"] = "Please select a payment method.";

                return await viewData();

                //return Page();
            }

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
                    Note = bookingInformation.Note != null ? bookingInformation.Note : "",
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
                if (SelectedPaymentMethod == "Cash")
                {
                    createCashPayment(newBookingInformation);
                    return RedirectToPage("/Index");
                }
                else if (SelectedPaymentMethod == "TransferCash")
                {
                    var vnPayModel = createTransferCashPayment(newBookingInformation);

                    var redirectUrl = _vnPayService.CreatePaymentUrl(HttpContext, vnPayModel);
                    try
                    {
                        var client = _httpClientFactory.CreateClient();
                        client.Timeout = TimeSpan.FromSeconds(30);

                        var response = client.GetAsync(redirectUrl).Result;
                        response.EnsureSuccessStatusCode();

                        return Redirect(redirectUrl);
                    }
                    catch (Exception ex)
                    {
                        return RedirectToPage("./PaymentResponse");
                    }
                }
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

        private async Task <IActionResult> viewData()
        {
            var booking = SessionHelper.GetObjectSession<BookingCreateReqModel>(HttpContext.Session, "BookingInformation");

            Booking = _mapper.Map<BookingInformationViewResModel>(booking);

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
                days = Math.Max(1, (int) end.Subtract(start).TotalDays);

                foreach (var service in petCareServices)
                {
                    var currentService = _petCareService.GetPetCareServiceById(service.Id);
                    totalServicePrice += currentService.Price;
                }

                var currAccommodation = _accommodationService.GetAccommodationById(booking.AccommodationId);

                totalPrice = (currAccommodation.Price + totalServicePrice) * days;

                AccommodationName = Booking.Accommodation;

                PetName = Booking.Pet;

                PetCareServices = _mapper.Map<List<PetCareViewListResModel>>(petCareServices);

                TotalPrice = totalPrice;
            }
            return Page();
        }

        private void createCashPayment(BookingInformation bookingInformation)
        {
            var currBooking = _bookingInformationService.GetBookingInformationById(bookingInformation.Id);

            decimal totalPrice = 0;

            decimal totalServicePrice = 0;

            int days;

            if (currBooking != null)
            {
                days = Math.Max(1, (int)currBooking.EndDate.Subtract(currBooking.StartDate).TotalDays);

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
                    Price = totalPrice,
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

        private VnPaymentRequestModel createTransferCashPayment(BookingInformation bookingInformation)
        {
            var currBooking = _bookingInformationService.GetBookingInformationById(bookingInformation.Id);

            decimal totalPrice = 0;

            decimal totalServicePrice = 0;

            int days;

            if (currBooking != null)
            {
                days = Math.Max(1, (int)currBooking.EndDate.Subtract(currBooking.StartDate).TotalDays);

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
                    Price = totalPrice,
                    Date = DateTime.Now,
                    Method = "TransferCash",
                    Status = PaymentStatusEnums.Unpaid.ToString(),
                    UserId = currBooking.UserId,
                    BookingId = currBooking.Id
                };

                _paymentRecordService.Add(paymentRecord);

                VnPaymentRequestModel vnpay = new VnPaymentRequestModel
                {
                    OrderId = paymentRecord.BookingId,
                    PaymentId = paymentRecord.Id,
                    Amount = (paymentRecord.Price),
                    CreatedDate = paymentRecord.Date
                };

                HttpContext.Session.Remove("BookingInformation");

                HttpContext.Session.Remove("SelectedPetCareServices");

                return vnpay;
            }
            return null;
        }
    }
}
