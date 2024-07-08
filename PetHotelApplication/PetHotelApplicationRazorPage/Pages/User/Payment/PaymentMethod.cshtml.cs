using BusinessObjects.Entities;
using BusinessObjects.Enums.PaymenStatusEnums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.AccommodationService;
using Services.Services.BookingInformationService;
using Services.Services.PaymentRecordService;
using Services.Services.PetCareServices;
using Services.Services.ServicesBookingService;
using System.ComponentModel.DataAnnotations;

namespace PetHotelApplicationRazorPage.Pages.User.Payment
{
    public class PaymentMethodModel : PageModel
    {
        private readonly IPetCareService _petCareService;
        private readonly IAccommodationService _accommodationService;
        private readonly IBookingInformationService _bookingInformationService;
        private readonly IPaymentRecordService _paymentRecordService;
        private readonly IServiceBookingService _serviceBookingService;

        [BindProperty]
        [Required(ErrorMessage = "Please select a payment method.")]
        public string SelectedPaymentMethod { get; set; }

        public BookingInformation BookingInformation { get; set; } = default!;

        public PaymentMethodModel(IPetCareService petCareService, 
            IAccommodationService accommodationService, 
            IBookingInformationService bookingInformationService,
            IPaymentRecordService paymentRecordService,
            IServiceBookingService serviceBookingService)
        {
            _petCareService = petCareService;
            _accommodationService = accommodationService;
            _bookingInformationService = bookingInformationService;
            _paymentRecordService = paymentRecordService;
            _serviceBookingService = serviceBookingService;
        }

        public IActionResult OnGet()
        {
            var booking = SessionHelper.GetObjectSession<BookingInformation>(HttpContext.Session, "BookingInformation");

            if (booking == null)
            {
                return RedirectToPage("/Forbidden");
            }

            BookingInformation = booking;

            return Page();
        }

        public IActionResult OnPostConfirm()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (SelectedPaymentMethod == "Cash")
            {
                BookingInformation bookingInformation = SessionHelper.GetObjectSession<BookingInformation>(HttpContext.Session, "BookingInformation");

                var selectedServiceId = SessionHelper.GetObjectSession<List<string>>(HttpContext.Session, "SelectedPetCareServices");

                if (bookingInformation != null)
                {
                    _bookingInformationService.Add(bookingInformation);

                    List<PetCareService> petCareServices = _petCareService.GetPetCareServicesByIds(selectedServiceId.ToList());

                    foreach (var item in petCareServices)
                    {
                        ServiceBooking serviceBooking = new ServiceBooking
                        {
                            Id = Guid.NewGuid().ToString(),
                            BookingId = bookingInformation.Id,
                            ServiceId = item.Id
                        };

                        _serviceBookingService.Add(serviceBooking);
                    }

                    createPayment(bookingInformation);
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

            return RedirectToPage("/User/Booking/Create");
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
