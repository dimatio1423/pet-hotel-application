using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.PetCareServices;
using Services.Services.ServiceImageService;
using Services.Services.ServicesBookingService;

namespace PetHotelApplicationRazorPage.Pages.PetCareServices.ServiceManage
{
    public class ServiceDetailsModel : AuthorizePageModel
    {
        private readonly IPetCareService _petCareService;
        private readonly IServiceImageService _serviceImageService;

        public ServiceDetailsModel(IPetCareService petCareService, IServiceImageService serviceImageService)
        {
            _petCareService = petCareService;
            _serviceImageService = serviceImageService;
        }

        public PetCareService Service { get; private set; }

        public List<ServiceImage> ServiceImage { get; set; }

        public void OnGet(string id)
        {
            Service = _petCareService.GetPetCareServiceById(id);

            ServiceImage = _serviceImageService.GetServiceImagesByServiceId(Service.Id);
        }
    }
}
