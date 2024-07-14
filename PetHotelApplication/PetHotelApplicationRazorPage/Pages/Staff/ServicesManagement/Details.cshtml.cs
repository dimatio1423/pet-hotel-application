using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Repositories;
using Services.Services.PetCareServices;
using BusinessObjects.Models.PetCareServiceModel.Request;
using BusinessObjects.Enums.StatusEnums;
using BusinessObjects.CustomValidators;
using System.ComponentModel.DataAnnotations;
using Services.Services.ServiceImageService;
using Services.Services.CloudinaryService;
using BusinessObjects.Enums.BoardingTypeEnums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetHotelApplicationRazorPage.Pages.Staff.ServicesManagement
{
    public class DetailsModel : AuthorizePageModel
    {
        private readonly IPetCareService _petCareService;
        private readonly IServiceImageService _serviceImageService;
        private readonly ICloudinaryService _cloudinaryService;

        public DetailsModel(IPetCareService petCareService, IServiceImageService serviceImageService, ICloudinaryService cloudinaryService)
        {
            _petCareService = petCareService;
            _serviceImageService = serviceImageService;
            _cloudinaryService = cloudinaryService;
        }

        [BindProperty]
        public PetCareServiceUpdateReqModel PetCareService { get; set; } = default!;

        public List<ServiceImage> ServiceImages { get; set; } = default!;

        [BindProperty]
        //[MinLength(1, ErrorMessage = "please choose at least one image for service")
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public List<IFormFile>? NewServiceImages { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petcareservice = _petCareService.GetPetCareServiceById(id);
            if (petcareservice == null)
            {
                return NotFound();
            }
            else
            {
                PetCareService = new PetCareServiceUpdateReqModel
                {
                    Id = petcareservice.Id,
                    Type = petcareservice.Type,
                    Description = petcareservice.Description,
                    Price = petcareservice.Price,
                    Status = petcareservice.Status
                };

                ViewData["ServiceStatus"] = new SelectList(new List<string> { StatusEnums.Available.ToString(), StatusEnums.Unavailable.ToString() }, petcareservice.Status);
                ServiceImages = petcareservice.ServiceImages.ToList();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        { 
            try
            {
                var updateService = _petCareService.GetPetCareServiceById(PetCareService.Id);

                //if (!ModelState.IsValid)
                //{
                //    ServiceImages = updateService.ServiceImages.ToList();
                //    return Page();
                //}

                if (String.IsNullOrEmpty(PetCareService.Type) || String.IsNullOrWhiteSpace(PetCareService.Type))
                {
                    TempData["ErrorType"] = "Service type can not be empty";
                    ViewData["ServiceStatus"] = new SelectList(new List<string> { StatusEnums.Available.ToString(), StatusEnums.Unavailable.ToString() }, updateService.Status);
                    ServiceImages = updateService.ServiceImages.ToList();
                    return Page();
                }

                if (String.IsNullOrEmpty(PetCareService.Description) || String.IsNullOrWhiteSpace(PetCareService.Description))
                {
                    TempData["ErrorDescription"] = "Description can not be empty";
                    ViewData["ServiceStatus"] = new SelectList(new List<string> { StatusEnums.Available.ToString(), StatusEnums.Unavailable.ToString() }, updateService.Status);
                    ServiceImages = updateService.ServiceImages.ToList();
                    return Page();
                }


                if (!Enum.IsDefined(typeof(StatusEnums), PetCareService.Status))
                {
                    TempData["ErrorStatus"] = "Invalid status type";
                    ViewData["ServiceStatus"] = new SelectList(new List<string> { StatusEnums.Available.ToString(), StatusEnums.Unavailable.ToString() }, updateService.Status);
                    ServiceImages = updateService.ServiceImages.ToList();
                    return Page();
                }

                if (!PetCareService.Status.Equals(StatusEnums.Available.ToString()) && !PetCareService.Status.Equals(StatusEnums.Unavailable.ToString()))
                {
                    TempData["ErrorStatus"] = "Service status only accept Available status and Unavailable status";
                    ViewData["ServiceStatus"] = new SelectList(new List<string> { StatusEnums.Available.ToString(), StatusEnums.Unavailable.ToString() }, updateService.Status);
                    ServiceImages = updateService.ServiceImages.ToList();
                    return Page();
                }

                if (!updateService.Type.Equals(PetCareService.Type))
                {
                    var currPetCareService = _petCareService.GetPetCareServiceByType(PetCareService.Type);

                    if (currPetCareService != null)
                    {
                        TempData["ErrorType"] = "Pet care service type is already existed";
                        ViewData["ServiceStatus"] = new SelectList(new List<string> { StatusEnums.Available.ToString(), StatusEnums.Unavailable.ToString() }, updateService.Status);
                        ServiceImages = updateService.ServiceImages.ToList();
                        return Page();
                    }
                }

                updateService.Type = PetCareService.Type;
                updateService.Description = PetCareService.Description;
                updateService.Price = PetCareService.Price;
                updateService.Status = PetCareService.Status.ToString();

                if (NewServiceImages != null && NewServiceImages.Count > 10)
                {
                    TempData["ErrorServiceImage"] = "Maximum ten images for service";
                    ViewData["ServiceStatus"] = new SelectList(new List<string> { StatusEnums.Available.ToString(), StatusEnums.Unavailable.ToString() }, updateService.Status);
                    ServiceImages = updateService.ServiceImages.ToList();
                    return Page();
                }

                if (NewServiceImages != null)
                {
                   if (NewServiceImages.Count() > 0)
                    {
                        foreach (var currImage in updateService.ServiceImages)
                        {
                            await _cloudinaryService.DeletePhoto(currImage.Image);
                            _serviceImageService.Delete(currImage);
                        }

                        foreach (var updateImage in NewServiceImages)
                        {
                            var result = await _cloudinaryService.AddPhoto(updateImage);

                            ServiceImage serviceImage = new ServiceImage
                            {
                                Id = Guid.NewGuid().ToString(),
                                Image = result.SecureUrl.ToString(),
                                ServiceId = updateService.Id
                            };

                            _serviceImageService.Add(serviceImage);
                        }
                    }
                }

                _petCareService.Update(updateService);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetCareServicExists(PetCareService.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Staff/ServicesManagement/Index");
        }

        private bool PetCareServicExists(string id)
        {
            return _petCareService.GetPetCareServiceById(id) != null ? true : false;
        }
    }
}
