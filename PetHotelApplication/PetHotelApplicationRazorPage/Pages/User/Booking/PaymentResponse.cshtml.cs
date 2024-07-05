using BusinessObjects.Enums.BookingStatusEnums;
using BusinessObjects.Enums.PaymenStatusEnums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.BookingInformationService;
using Services.Services.PaymentRecordService;
using Services.Services.VnPaymentServices;

namespace PetHotelApplicationRazorPage.Pages.User.Booking
{
    public class PaymentResponseModel : PageModel
    {
        private readonly IVnPayService _vnPayService;
        private readonly IPaymentRecordService _paymentRecordService;
        private readonly IBookingInformationService _bookingInformationService;

        public PaymentResponseModel(IVnPayService vnPayService, 
            IPaymentRecordService paymentRecordService,
            IBookingInformationService bookingInformationService)
        {
            _vnPayService = vnPayService;
            _paymentRecordService = paymentRecordService;
            _bookingInformationService = bookingInformationService;
        }

        public IActionResult OnGet()
        {
            var response = _vnPayService.PaymentResponse(Request.Query);
            var paymentRecord = _paymentRecordService.GetPaymentRecordById(response.PaymentId);
            var booking = _bookingInformationService.GetBookingInformationById(response.OrderId);

            if (response == null || paymentRecord == null || booking == null)
            {
                return NotFound();
            }

            if (response.VnPayResponseCode.Equals("00"))
            {
                paymentRecord.Status = nameof(PaymentStatusEnums.Paid);
                booking.Status = nameof(BookingStatusEnums.Completed);
            }
            else
            {
                paymentRecord.Status = nameof(PaymentStatusEnums.Error);
            }

            _paymentRecordService.Update(paymentRecord);
            _bookingInformationService.Update(booking);

            return RedirectToPage("./PaymentRecords", new 
            {
                bookingId = booking.Id
            });
        }
    }
}
