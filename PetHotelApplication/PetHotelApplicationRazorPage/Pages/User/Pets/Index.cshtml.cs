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
    public class IndexModel : PageModel
    {
        private readonly IPetService _petService;
            
        public IndexModel(IPetService petService)
        {
            _petService = petService;
        }

        public IList<PetResModel> Pet { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Pet = _petService.GetActivePets("2");
        }
    }
}
