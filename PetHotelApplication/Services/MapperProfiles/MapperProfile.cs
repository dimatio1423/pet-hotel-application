using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.Models.PetCareModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PetCareService, PetCareResModel>();
        }
    }
}
