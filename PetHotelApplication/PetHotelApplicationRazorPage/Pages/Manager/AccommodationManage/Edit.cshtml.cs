using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.AccommodationService;
using BusinessObjects.Enums.AccommodationTypeEnums;
using BusinessObjects.Models.AccommodationModel.Request;
using AutoMapper;

namespace PetHotelApplicationRazorPage.Pages.Manager.AccommodationManage
{
    public class EditModel : AuthorizePageModel
    {
        private readonly IAccommodationService _accommodationService;
        private readonly IMapper _mapper;

        public EditModel(IAccommodationService accommodationService, IMapper mapper)
        {
            _accommodationService = accommodationService;
            _mapper = mapper;
        }

        [BindProperty]
        public AccommodationUpdateReqModel Accommodation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accommodation = _accommodationService.GetAccommodationById(id);
            if (accommodation == null)
            {
                return NotFound();
            }
            var enumList = Enum.GetValues(typeof(AccommodationTypeEnums)).Cast<AccommodationTypeEnums>()
                               .Select(e => new AccommodationTypeEnumModel
                               {
                                   EnumId = (int)e,
                                   EnumValue = e.ToString()
                               }).ToList();
            Accommodation = _mapper.Map<AccommodationUpdateReqModel>(accommodation);

            ViewData["AccommodationType"] = new SelectList(enumList, "EnumValue", "EnumValue", accommodation.Type);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var accommodation = _accommodationService.GetAccommodationById(Accommodation.Id);
            _mapper.Map(Accommodation, accommodation);
            var allAccommodationNames = _accommodationService.GetAccommodations().Where(a => a.Id != Accommodation.Id)
                                                             .Select(a => a.Name.ToLower())
                                                             .ToList();

            if (allAccommodationNames.Contains(accommodation.Name.ToLower()))
            {
                ModelState.AddModelError(string.Empty, "Accommodation name is already existed");
                return Page();
            }
            _accommodationService.Update(accommodation);

            return RedirectToPage("./Index");
        }
    }
}
