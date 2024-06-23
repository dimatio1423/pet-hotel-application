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

namespace PetHotelApplicationRazorPage.Pages.PetCareServices.ServiceManage
{
    public class ServicesModel : AuthorizePageModel
    {
        private readonly IPetCareService _petCareService;
        private readonly IMapper _mapper;

        public ServicesModel(IPetCareService petCareService, IMapper mapper)
        {
            _petCareService = petCareService;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchServices { get; set; }

        public IList<PetCareResModel> PetCareService { get; set; } = new List<PetCareResModel>();

        public async Task OnGetAsync()
        {
            var list = _petCareService.GetPetCareServices();

            if (!string.IsNullOrEmpty(SearchServices))
            {
                list = list.Where(l => l.Type.ToLower().Contains(SearchServices.ToLower())).ToList();
            }

            PetCareService = _mapper.Map<List<PetCareResModel>>(list);
        }
    }

}
