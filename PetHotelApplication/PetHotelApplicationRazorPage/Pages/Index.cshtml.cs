using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Enums.RoleEnums;
using BusinessObjects.Models.PetCareModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.PetCareServices;
using Services.Services.UserService;

namespace PetHotelApplicationRazorPage.Pages
{
    public class IndexModel : AuthorizePageModel
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
        public async Task<IActionResult> OnGet()
        {
            var currUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");
            if (currUser != null)
            {
                currentUser = currUser;
            }

            var list = _petCareService.GetPetCareServices();
            PetCareServices = _mapper.Map<List<PetCareResModel>>(list);
            return Page();
        }
    }
}
