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
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Enums.BookingStatusEnums;
using BusinessObjects.Enums.PaymenStatusEnums;

namespace PetHotelApplicationRazorPage.Pages.Staff.PaymentRecordManagement
{
    public class IndexModel : AuthorizePageModel
    {
        private readonly IPaymentRecordService _paymentRecordService;

        public IndexModel(IPaymentRecordService paymentRecordService)
        {
            _paymentRecordService = paymentRecordService;
        }

        public PaginatedList<PaymentRecord> PaymentRecord { get;set; } = default!;

        public SelectList PaymentStatus { get; set; }
        public SelectList PaymentMethod { get; set; }
        public string StatusFilter { get; set; }
        public string MethodFilter { get; set; }
        public string CurrentFilter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        private const int pageSize = 4;


        public async Task OnGetAsync(string currentFilter, string searchString, int? pageIndex,
            DateTime? startDate, DateTime? endDate, string status, string method)
        {

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            StartDate = startDate;
            EndDate = endDate;

            StatusFilter = status;

            MethodFilter = method;

            var paymentRecords = _paymentRecordService.GetPaymentRecords().OrderByDescending(x => x.Date).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                paymentRecords = paymentRecords.Where(e => e.User.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(StatusFilter))
            {
                paymentRecords = paymentRecords.Where(e => e.Status.Equals(StatusFilter)).ToList();
            }

            if (!string.IsNullOrEmpty(MethodFilter))
            {
                paymentRecords = paymentRecords.Where(e => e.Method.Equals(MethodFilter)).ToList();
            }

            if (startDate.HasValue)
            {
                paymentRecords = paymentRecords.Where(e => e.Date >= startDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                paymentRecords = paymentRecords.Where(e => e.Date <= endDate.Value).ToList();
            }

            PaymentRecord = PaginatedList<PaymentRecord>.Create(paymentRecords, pageIndex ?? 1, pageSize);

            PaymentStatus = new SelectList(Enum.GetNames(typeof(PaymentStatusEnums)), StatusFilter);

            PaymentMethod = new SelectList(new List<string>{"Cash", "TransferCash"}, MethodFilter);

        }
    }
}
