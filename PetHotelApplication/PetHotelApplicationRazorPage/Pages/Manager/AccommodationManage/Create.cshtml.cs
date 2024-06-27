using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.AccommodationService;
using BusinessObjects.Models.AccommodationModel.Request;
using AutoMapper;
using BusinessObjects.Enums.StatusEnums;
using BusinessObjects.Enums.AccommodationTypeEnums;

namespace PetHotelApplicationRazorPage.Pages.Manager.AccommodationManage
{
    public class CreateModel : PageModel
    {
        private readonly IAccommodationService _accommodationService;
        private readonly IMapper _mapper;

        public CreateModel(IAccommodationService accommodationService, IMapper mapper)
        {
            _accommodationService = accommodationService;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            var enumList = Enum.GetValues(typeof(AccommodationTypeEnums)).Cast<AccommodationTypeEnums>()
                               .Select(e => new AccommodationTypeEnumModel
                               {
                                   EnumId = (int)e,
                                   EnumValue = e.ToString()
                               }).ToList();

            ViewData["AccommodationType"] = new SelectList(enumList, "EnumValue", "EnumValue");
            return Page();
        }

        [BindProperty]
        public AccommodationCreateReqModel Accommodation { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var newAccommodation = _mapper.Map<Accommodation>(Accommodation);
            newAccommodation.Id = Guid.NewGuid().ToString();
            newAccommodation.Status = StatusEnums.Available.ToString();
            _accommodationService.Add(newAccommodation);

            return RedirectToPage("./Index");
        }
    }
}
