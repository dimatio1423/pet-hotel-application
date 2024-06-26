﻿using System;
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

        public PaginatedList<PetCareService> PetCareService { get;set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            var services = _petCareService.GetPetCareServices();
            PetCareService = PaginatedList<PetCareService>.Create(services, pageIndex ?? 1, pageSize);
        }
    }
}