using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.PaymentRecordService;
using BusinessObjects.Models.PaymentRecordModel.Response;

namespace PetHotelApplicationRazorPage.Pages.User.Booking
{
    public class PaymentRecordsModel : AuthorizePageModel
    {
        private readonly IPaymentRecordService _paymentRecordService;

        public PaymentRecordsModel(IPaymentRecordService paymentRecordService)
        {
            _paymentRecordService = paymentRecordService;
        }

        public IList<PaymentRecordResModel> PaymentRecords { get;set; } = default!;

        public async Task OnGetAsync(string bookingId)
        {
            PaymentRecords = _paymentRecordService.GetPaymentRecordsFromBookingId(bookingId);
        }
    }
}
