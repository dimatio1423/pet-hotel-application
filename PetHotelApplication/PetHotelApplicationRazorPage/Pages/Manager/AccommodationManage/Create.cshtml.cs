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
    public class CreateModel : AuthorizePageModel
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
                var enumList = Enum.GetValues(typeof(AccommodationTypeEnums)).Cast<AccommodationTypeEnums>()
                               .Select(e => new AccommodationTypeEnumModel
                               {
                                   EnumId = (int)e,
                                   EnumValue = e.ToString()
                               }).ToList();

                ViewData["AccommodationType"] = new SelectList(enumList, "EnumValue", "EnumValue");
                return Page();
            }
            var newAccommodation = _mapper.Map<Accommodation>(Accommodation);
            var allAccommodationNames = _accommodationService.GetAccommodations().Select(a => a.Name.ToLower()).ToList();
            if (allAccommodationNames.Contains(newAccommodation.Name.ToLower()))
            {
                var enumList = Enum.GetValues(typeof(AccommodationTypeEnums)).Cast<AccommodationTypeEnums>()
                               .Select(e => new AccommodationTypeEnumModel
                               {
                                   EnumId = (int)e,
                                   EnumValue = e.ToString()
                               }).ToList();

                ViewData["AccommodationType"] = new SelectList(enumList, "EnumValue", "EnumValue");
                //ModelState.AddModelError(string.Empty, "Accommodation name is already existed");
                TempData["ErrorName"] = "Accommodation name is already exist";
                return Page();
            }

            if ((newAccommodation.Type.Equals(AccommodationTypeEnums.Kennel.ToString()) 
                || newAccommodation.Type.Equals(AccommodationTypeEnums.Suite.ToString())) && newAccommodation.Capacity > 2)
            {
                var enumList = Enum.GetValues(typeof(AccommodationTypeEnums)).Cast<AccommodationTypeEnums>()
                               .Select(e => new AccommodationTypeEnumModel
                               {
                                   EnumId = (int)e,
                                   EnumValue = e.ToString()
                               }).ToList();

                ViewData["AccommodationType"] = new SelectList(enumList, "EnumValue", "EnumValue");
                //ModelState.AddModelError(string.Empty, "For Kennel and Suite, the capacity only accepts a maximum of 2");
                TempData["ErrorCapacity"] = "For Kennel and Suite, the capacity only accepts a maximum of 2";
                return Page();
            }

            newAccommodation.Id = Guid.NewGuid().ToString();
            newAccommodation.Status = StatusEnums.Available.ToString();
            _accommodationService.Add(newAccommodation);

            return RedirectToPage("./Index");
        }
    }
}
