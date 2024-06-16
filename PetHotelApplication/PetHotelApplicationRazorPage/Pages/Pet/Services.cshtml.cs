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
using BusinessObjects.Models.PetCareModel.Response;
using AutoMapper;

namespace PetHotelApplicationRazorPage.Pages.Pet
{
    public class ServicesModel : PageModel
    {
        private readonly IPetCareService _petCareService;
        private readonly IMapper _mapper;

        public ServicesModel(IPetCareService petCareService,
                             IMapper mapper)
        {
            _petCareService = petCareService;
            _mapper = mapper;
        }

        public IList<PetCareResModel> PetCareService { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var list = _petCareService.GetPetCareServices();
            PetCareService = _mapper.Map<List<PetCareResModel>>(list);
        }
    }
}
