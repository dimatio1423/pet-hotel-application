using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Services.Services.PetCareServices;
using Repositories;
using BusinessObjects.Models.PetCareModel.Response;
using AutoMapper;
using Services.Services.FeedbackService;

namespace PetHotelApplicationRazorPage.Pages.PetCareServices.ServiceManage
{
    public class ServicesModel : AuthorizePageModel
    {
        private readonly IPetCareService _petCareService;
        private readonly IMapper _mapper;
        private readonly IFeedbackService _feedbackService;

        public ServicesModel(IPetCareService petCareService, IMapper mapper, IFeedbackService feedbackService)
        {
            _petCareService = petCareService;
            _mapper = mapper;
            _feedbackService = feedbackService;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchServices { get; set; }

        public IList<PetCareResModel> PetCareService { get; set; } = new List<PetCareResModel>();
        public IList<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        public async Task OnGetAsync()
        {
            var list = _petCareService.GetPetCareServices();

            var feedbacks = _feedbackService.GetFeedbacks();

            if (!string.IsNullOrEmpty(SearchServices))
            {
                list = list.Where(l => l.Type.ToLower().Contains(SearchServices.ToLower())).ToList();
            }

            PetCareService = _mapper.Map<List<PetCareResModel>>(list);

            Feedbacks = feedbacks;
        }
    }

}
