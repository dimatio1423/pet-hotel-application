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
        public string CurrentSort { get; set; }
        public string NameSort { get; set; }
        public string PhoneSort { get; set; }
        public string EmailSort { get; set; }
        public string StatusSort { get; set; }
        public string RoleSort { get; set; }
        public string AddressSort { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchValue, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            PhoneSort = sortOrder == "phone_asc" ? "phone_desc" : "phone_asc";
            EmailSort = sortOrder == "email_asc" ? "email_desc" : "email_asc";
            StatusSort = sortOrder == "status_asc" ? "status_desc" : "status_asc";
            RoleSort = sortOrder == "role_asc" ? "role_desc" : "role_asc";
            AddressSort = sortOrder == "address_asc" ? "address_desc" : "address_asc";

            if (searchValue != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchValue = currentFilter;
            }

            CurrentFilter = searchValue;

            var users = _userService.GetUsers(searchValue, sortOrder);
            Users = PaginatedList<BusinessObjects.Entities.User>.Create(users, pageIndex ?? 1, 5);
        }
    }
}