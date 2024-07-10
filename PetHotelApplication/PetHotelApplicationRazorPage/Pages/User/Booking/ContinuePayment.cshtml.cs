using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Enums.BookingStatusEnums;
using BusinessObjects.Enums.PaymenStatusEnums;
using BusinessObjects.Models.BookingInformationModel.Response;
using BusinessObjects.Models.VnPaymentModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.BookingInformationService;
using Services.Services.PaymentRecordService;
using Services.Services.VnPaymentServices;
using System.ComponentModel.DataAnnotations;

namespace PetHotelApplicationRazorPage.Pages.User.Booking
{
    public class ContinuePaymentModel : PageModel
    {
        private readonly IBookingInformationService _bookingInformationService;
        private readonly IPaymentRecordService _paymentRecordService;
        private readonly IMapper _mapper;
        private readonly IVnPayService _vnPayService;
        private readonly IHttpClientFactory _httpClientFactory;

        public ContinuePaymentModel(IBookingInformationService bookingInformationService,
                                    IPaymentRecordService paymentRecordService,
                                    IMapper mapper,
                                    IVnPayService vnPayService
,
                                    IHttpClientFactory httpClientFactory)
        {
            _bookingInformationService = bookingInformationService;
            _paymentRecordService = paymentRecordService;
            _mapper = mapper;
            _vnPayService = vnPayService;
            _httpClientFactory = httpClientFactory;
        }

        public BookingContinuePaymentResModel BookingInfo { get; set; }

        [BindProperty]
        public string BookingId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select a payment method.")]
        public string SelectedPaymentMethod { get; set; } = null!;

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

            if (!currentBooking.Status.Equals(nameof(BookingStatusEnums.Pending)))
            {
                return RedirectToPage("/Forbidden");
            }

            BookingInfo = _mapper.Map<BookingContinuePaymentResModel>(currentBooking);
            BookingId = BookingInfo.Id;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var currentBooking = _bookingInformationService.GetBookingInformationById(BookingId);
            BookingInfo = _mapper.Map<BookingContinuePaymentResModel>(currentBooking);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currentUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");
            PaymentRecord paymentRecord = new PaymentRecord
            {
                Id = Guid.NewGuid().ToString(),
                Price = BookingInfo.TotalPrice,
                Date = DateTime.Now,
                Method = SelectedPaymentMethod,
                Status = PaymentStatusEnums.Unpaid.ToString(),
                UserId = currentUser.Id,
                BookingId = BookingInfo.Id
            };

            _paymentRecordService.CancelBookingPaymentRecords(paymentRecord.BookingId);
            _paymentRecordService.Add(paymentRecord);

            if (SelectedPaymentMethod.Equals("Cash"))
            {
                return RedirectToPage("./BookingHistory");
            }

            VnPaymentRequestModel vnpay = new VnPaymentRequestModel
            {
                OrderId = paymentRecord.BookingId,
                PaymentId = paymentRecord.Id,
                Amount = (paymentRecord.Price),
                CreatedDate = paymentRecord.Date
            };

            var redirectUrl = _vnPayService.CreatePaymentUrl(HttpContext, vnpay);
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

        public IActionResult OnPostCancelBooking()
        {
            _paymentRecordService.CancelBookingPaymentRecords(BookingId);

            var currentBooking = _bookingInformationService.GetBookingInformationById(BookingId);
            currentBooking.Status = nameof(BookingStatusEnums.Cancelled);
            _bookingInformationService.Update(currentBooking);

            return RedirectToPage("./BookingHistory");
        }
    }
}
