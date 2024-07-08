using Repositories.Repositories.AccommodationRepo;
using Repositories.Repositories.BookingInformationRepo;
using Repositories.Repositories.FeedbackRepo;
using Repositories.Repositories.PetCareServiceRepo;
using Repositories.Repositories.UserRepo;
using System.Linq;

namespace Services.Services.DashboardService
{
    public class DashboardService : IDashboardService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookingInformationRepository _bookingRepository;
        private readonly IPetCareServiceRepository _serviceRepository;
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IFeedbackRepository _feedbackRepository;

        public DashboardService(
            IUserRepository userRepository,
            IBookingInformationRepository bookingRepository,
            IPetCareServiceRepository serviceRepository,
            IAccommodationRepository accommodationRepository,
            IFeedbackRepository feedbackRepository)
        {
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _serviceRepository = serviceRepository;
            _accommodationRepository = accommodationRepository;
            _feedbackRepository = feedbackRepository;
        }

        public int GetTotalUsers()
        {
            return _userRepository.GetUsers().Count();
        }

        public int GetTotalBookings()
        {
            return _bookingRepository.GetBookingInformations().Count();
        }

        public int GetTotalServices()
        {
            return _serviceRepository.GetPetCareServices().Count();
        }

        public int GetTotalAccommodations()
        {
            return _accommodationRepository.GetAccommodations().Count();
        }

        public int[] GetFeedbackCountsByRating()
        {
            var feedbacks = _feedbackRepository.GetFeedbacks();
            var counts = new int[5];
            for (int i = 1; i <= 5; i++)
            {
                counts[i - 1] = feedbacks.Count(f => f.Rating == i);
            }
            return counts;
        }
    }
}
