using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.PetCareServices;

namespace PetHotelApplicationRazorPage.Pages.Pet
{
    public class ServiceDetailsModel : PageModel
    {
        private readonly IPetCareService _petCareService;

        public ServiceDetailsModel(IPetCareService petCareService)
        {
            _petCareService = petCareService;
        }

        public PetCareService Service { get; private set; }

        public void OnGet(string id)
        {
            Service = _petCareService.GetPetCareServiceById(id);
        }
    }
}
