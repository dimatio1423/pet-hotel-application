using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.PetService;
using BusinessObjects.Models.PetModel.Response;

namespace PetHotelApplicationRazorPage.Pages.User.Pets
{
    public class IndexModel : AuthorizePageModel
    {
        private readonly IPetService _petService;
            
        public IndexModel(IPetService petService)
        {
            _petService = petService;
        }

        public PaginatedList<PetResModel> Pet { get;set; } = default!;

        public string CurrentFilter { get; set; }

        private const int pageSize = 4;

        public async Task OnGetAsync(string currentFilter, string SearchValue, int? pageIndex)
        {
            if (SearchValue != null)
            {
                pageIndex = 1;
            }
            else
            {
                SearchValue = currentFilter;
            }

            CurrentFilter = SearchValue;

            var currentUser = HttpContext.Session.GetObjectSession<BusinessObjects.Entities.User>("Account");
            var list = _petService.GetActivePets(currentUser.Id, SearchValue);

            Pet = PaginatedList<PetResModel>.Create(list, pageIndex ?? 1, pageSize);
        }
    }
}
