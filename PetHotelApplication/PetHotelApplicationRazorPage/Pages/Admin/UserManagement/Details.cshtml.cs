using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Repositories;

namespace PetHotelApplicationRazorPage.Pages.Admin.UserManagement
{
    public class DetailsModel : PageModel
    {
        private readonly Repositories.PetHotelApplicationDbContext _context;

        public DetailsModel(Repositories.PetHotelApplicationDbContext context)
        {
            _context = context;
        }

        public BusinessObjects.Entities.User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }
    }
}
