using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Enums.BookingStatusEnums;
using BusinessObjects.Enums.PaymenStatusEnums;
using BusinessObjects.Enums.StatusEnums;
using BusinessObjects.Models.BookingInformationModel.Response;
using Repositories.Repositories.BookingInformationRepo;
using Repositories.Repositories.PaymentRecordRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.BookingInformationService
{
    public class BookingInformatonService : IBookingInformationService
    {
        private readonly IBookingInformationRepository _bookingInformationRepo;
        private readonly IPaymentRecordRepository _paymentRecordRepository;
        private readonly IMapper _mapper;

        public BookingInformatonService(IBookingInformationRepository bookingInformationRepo, 
            IPaymentRecordRepository paymentRecordRepository,
            IMapper mapper)
        {
            _bookingInformationRepo = bookingInformationRepo;
            _paymentRecordRepository = paymentRecordRepository;
            _mapper = mapper;
        }
        public void Add(BookingInformation bookingInformation)
        {
            _bookingInformationRepo.Add(bookingInformation);
        }

        public void Delete(BookingInformation bookingInformation)
        {
            _bookingInformationRepo.Delete(bookingInformation);
        }

        public BookingInformation GetBookingInformationById(string id)
        {
           return _bookingInformationRepo.GetBookingInformationById(id);
        }

        public List<BookingInformation> GetBookingInformations()
        {
            return _bookingInformationRepo.GetBookingInformations();
        }

        public void Update(BookingInformation bookingInformation)
        {
            _bookingInformationRepo.Update(bookingInformation);
        }

        public List<BookingInformationResModel> GetBookingInformationByUserId(string userId)
        {
            var list = _bookingInformationRepo.GetBookingInformationByUserId(userId);
            return _mapper.Map<List<BookingInformationResModel>>(list);
        }

        public string autoUpdatingBookingInformationStatus()
        {
            var now = DateTime.Now;

            var allBookings = _bookingInformationRepo.GetBookingInformations()
                .Where(x => x.Status == BookingStatusEnums.Pending.ToString() ||
                            x.Status == BookingStatusEnums.Confirmed.ToString() ||
                            x.Status == BookingStatusEnums.Processing.ToString())
                .ToList();

            foreach (var booking in allBookings)
            {
                if (booking.Status == BookingStatusEnums.Pending.ToString())
                {
                    if (!booking.PaymentRecords.Any(x => x.Status == PaymentStatusEnums.Paid.ToString()) && booking.StartDate.Day < now.Day)
                    {
                        booking.Status = BookingStatusEnums.Cancelled.ToString();
                        booking.ModifiedDate = DateTime.Now;
                        _bookingInformationRepo.Update(booking);

                        foreach(var paymentBooking in booking.PaymentRecords)
                        {
                            paymentBooking.Status = PaymentStatusEnums.Cancelled.ToString();
                            _paymentRecordRepository.Update(paymentBooking);
                        }
                    }
                }
                else if (booking.Status == BookingStatusEnums.Confirmed.ToString())
                {
                    if (booking.StartDate.Day <= now.Day && booking.EndDate.Day >= now.Day)
                    {
                        booking.Status = BookingStatusEnums.Processing.ToString();
                        booking.ModifiedDate = DateTime.Now;
                        _bookingInformationRepo.Update(booking);
                    }
                }
                else if (booking.Status == BookingStatusEnums.Processing.ToString())
                {
                    if (booking.EndDate.Day < now.Day)
                    {
                        booking.Status = BookingStatusEnums.Completed.ToString();
                        booking.ModifiedDate = DateTime.Now;
                        _bookingInformationRepo.Update(booking);
                    }
                }
            }

            return "Update successfully";
        }
            
    }
}
