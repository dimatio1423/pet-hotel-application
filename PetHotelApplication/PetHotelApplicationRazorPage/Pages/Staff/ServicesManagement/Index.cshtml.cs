using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.PetCareServices;
using BusinessObjects.Enums.StatusEnums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetHotelApplicationRazorPage.Pages.Staff.ServicesManagement
{
    public class IndexModel : AuthorizePageModel
    {
        private readonly IPetCareService _petCareService;

        private const int pageSize = 4;

        public IndexModel(IPetCareService petCareService)
        {
            _petCareService = petCareService;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchServices { get; set; }

        public SelectList Status { get; set; }

        public string StatusFilter { get; set; }

        public PaginatedList<PetCareService> PetCareService { get;set; } = default!;

        public async Task OnGetAsync(int? pageIndex, string status)
        {
            var services = _petCareService.GetPetCareServices();

            StatusFilter = status;

            if (!string.IsNullOrEmpty(StatusFilter))
            {
                services = services.Where(x => x.Status.Equals(StatusFilter)).ToList();
            }

            if (!string.IsNullOrEmpty(SearchServices))
            {
                services = services.Where(x => x.Type.ToLower().Contains(SearchServices.ToLower())).ToList();
            }

            Status = new SelectList(new List<string> { StatusEnums.Available.ToString(), StatusEnums.Unavailable.ToString()});

            PetCareService = PaginatedList<PetCareService>.Create(services, pageIndex ?? 1, pageSize);
        }
    }
}
