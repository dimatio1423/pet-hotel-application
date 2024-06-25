using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.UserService;

namespace PetHotelApplicationRazorPage.Pages.Admin.UserManagement
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public IList<BusinessObjects.Entities.User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = _userService.GetUsers();
        }
    }
}
