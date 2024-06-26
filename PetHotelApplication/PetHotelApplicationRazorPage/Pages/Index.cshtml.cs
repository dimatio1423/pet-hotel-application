using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Enums.RoleEnums;
using BusinessObjects.Models.PetCareModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.FeedbackService;
using Services.Services.PetCareServices;
using Services.Services.UserService;

namespace PetHotelApplicationRazorPage.Pages
{
    public class IndexModel : AuthorizePageModel
    {
        public IndexModel(IUserService userService, 
            IPetCareService petCareService,
            IMapper mapper, 
            IFeedbackService feedbackService)
        {
            _userService = userService;
            _petCareService = petCareService;
            _mapper = mapper;
            _feedbackService = feedbackService;
        }
        private readonly ILogger<IndexModel> _logger;
        private readonly IUserService _userService;
        private readonly IPetCareService _petCareService;
        private readonly IMapper _mapper;
        private readonly IFeedbackService _feedbackService;

        [BindProperty]
        public BusinessObjects.Entities.User currentUser { get; set; }

        public IList<PetCareResModel> PetCareServices { get; set; } = default;

        public IList<Feedback> feedbacks { get; set; } = default;

        public async Task<IActionResult> OnGet()
        {
            var currUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");
            if (currUser != null)
            {
                currentUser = currUser;
            }

            var list = _petCareService.GetPetCareServices();
            PetCareServices = _mapper.Map<List<PetCareResModel>>(list);

            feedbacks = _feedbackService.GetFeedbacks();

            return Page();
        }
    }
}
