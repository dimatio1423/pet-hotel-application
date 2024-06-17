using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Models.PetCareModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.PetCareServices;
using Services.Services.UserService;

namespace PetHotelApplicationRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(IUserService userService, IPetCareService petCareService, IMapper mapper)
        {
            _userService = userService;
            _petCareService = petCareService;
            _mapper = mapper;
        }
        private readonly ILogger<IndexModel> _logger;
        private readonly IUserService _userService;
        private readonly IPetCareService _petCareService;
        private readonly IMapper _mapper;

        [BindProperty]
        public BusinessObjects.Entities.User currentUser { get; set; }

        public IList<PetCareResModel> PetCareServices { get; set; } = default;
        public void OnGet()
        {
            if (HttpContext.Session.GetString("AccountEmail") != null)
            {
                var currentAccountEmail = HttpContext.Session.GetString("AccountEmail");
                var currUser = _userService.GetUserByEmail(currentAccountEmail);
                if (currUser != null)
                {
                    currentUser = currUser;
                }
            }

            var list = _petCareService.GetPetCareServices();
            PetCareServices = _mapper.Map<List<PetCareResModel>>(list);

        }
    }
}
