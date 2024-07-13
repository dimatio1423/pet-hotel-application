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
using Services.Services.BookingInformationService;
using BusinessObjects.Enums.StatusEnums;
using BusinessObjects.Enums.BookingStatusEnums;

namespace PetHotelApplicationRazorPage.Pages.Staff.PaymentRecordManagement
{
    public class DetailsModel : AuthorizePageModel
    {
        private readonly IPaymentRecordService _paymentRecordService;
        private readonly IBookingInformationService _bookingInformationService;

        public DetailsModel(IPaymentRecordService paymentRecordService,
            IBookingInformationService bookingInformationService)
        {
            _paymentRecordService = paymentRecordService;
            _bookingInformationService = bookingInformationService;
        }

        [BindProperty]
        public PaymentRecord PaymentRecord { get; set; } = default!;

        public SelectList PaymentStatus { get; set; }

        public SelectList PaymentMethod { get; set; }

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
                PaymentMethod = new SelectList(new List<string> { "Cash", "TransferCash" });
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

                    updatingPayment.Method = PaymentRecord.Method;

                    _paymentRecordService.Update(updatingPayment);

                    var bookingList = _bookingInformationService.GetBookingInformations();

                    switch(updatingPayment.Status)
                    {
                        case nameof(PaymentStatusEnums.Paid):
                            var currBooking = bookingList.FirstOrDefault(x => x.Id.Equals(updatingPayment.BookingId));

                            if (currBooking != null)
                            {
                                currBooking.Status = BookingStatusEnums.Confirmed.ToString();

                                currBooking.ModifiedDate = DateTime.Now;

                                _bookingInformationService.Update(currBooking);
                            }
                            break;

                        //case nameof(PaymentStatusEnums.Cancelled):
                        //     currBooking = bookingList.FirstOrDefault(x => x.Id.Equals(updatingPayment.BookingId));

                        //    if (currBooking != null)
                        //    {
                        //        currBooking.Status = BookingStatusEnums.Cancelled.ToString();

                        //        currBooking.ModifiedDate = DateTime.Now;

                        //        _bookingInformationService.Update(currBooking);
                        //    }
                        //    break;
                    }
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
