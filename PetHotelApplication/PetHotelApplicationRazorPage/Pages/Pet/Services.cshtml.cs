using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Services.Services.PetCareServices;
using Repositories;

namespace PetHotelApplicationRazorPage.Pages.Pet
{
    public class ServicesModel : PageModel
    {
        private readonly IPetCareService _petCareService;

        public ServicesModel(IPetCareService petCareService)
        {
            _petCareService = petCareService;
        }

        public IList<PetCareService> PetCareService { get;set; } = default!;

        public async Task OnGetAsync()
        {
            PetCareService = _petCareService.GetPetCareServices();
        }
    }
}
