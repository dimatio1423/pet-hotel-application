using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Services.UserService;

namespace PetHotelApplicationRazorPage.Pages.Admin.UserManagement
{
    public class IndexModel : AuthorizePageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {      
            _userService = userService;
        }

        public PaginatedList<BusinessObjects.Entities.User> Users { get; set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            var users = _userService.GetUsers();
            Users = PaginatedList<BusinessObjects.Entities.User>.Create(users, pageIndex ?? 1, 10);
        }
    }
}
