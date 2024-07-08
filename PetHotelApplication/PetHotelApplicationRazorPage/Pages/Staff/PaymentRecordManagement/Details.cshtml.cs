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
using BusinessObjects.Enums.PaymenStatusEnums;

namespace PetHotelApplicationRazorPage.Pages.Staff.PaymentRecordManagement
{
    public class DetailsModel : AuthorizePageModel
    {
        private readonly IPaymentRecordService _paymentRecordService;

        public DetailsModel(IPaymentRecordService paymentRecordService)
        {
            _paymentRecordService = paymentRecordService;
        }

        [BindProperty]
        public PaymentRecord PaymentRecord { get; set; } = default!;

        public SelectList PaymentStatus { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentrecord = _paymentRecordService.GetPaymentRecordById(id);
            if (paymentrecord == null)
            {
                return NotFound();
            }
            else
            {
                PaymentRecord = paymentrecord;
                PaymentStatus = new SelectList(new List<string> { PaymentStatusEnums.Paid.ToString(),
                    PaymentStatusEnums.Unpaid.ToString(), PaymentStatusEnums.Cancelled.ToString()}, PaymentRecord.Status.ToString());
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            try
            {
                var updatingPayment = _paymentRecordService.GetPaymentRecordById(PaymentRecord.Id);
                if (updatingPayment == null)
                {
                    return NotFound();
                }
                else
                {
                    updatingPayment.Status = PaymentRecord.Status;

                    _paymentRecordService.Update(updatingPayment);
                }


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExisted(PaymentRecord.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Staff/PaymentRecordManagement/Index");
        }

        private bool PaymentExisted(string id)
        {
            return _paymentRecordService.GetPaymentRecordById(id) != null ? true : false;
        }
    }
}
