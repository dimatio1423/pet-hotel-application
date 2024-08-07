﻿using BusinessObjects.Entities;
using Repositories.Repositories.AccommodationRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.AccommodationService
{
    public class AccommodationService : IAccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public AccommodationService(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }
        public void Add(Accommodation accommodation)
        {
            _accommodationRepository.Add(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodationRepository.Delete(accommodation);
        }

        public Accommodation GetAccommodationById(string id)
        {
            return _accommodationRepository.GetAccommodationById(id);
        }

        public Accommodation GetAccommodationByType(string type)
        {
            return _accommodationRepository.GetAccommodationByType(type);
        }

        public List<Accommodation> GetAccommodations()
        {
            return _accommodationRepository.GetAccommodations();
        }

        public List<Accommodation> GetAccommodationsWithSearchSort(string SearchValue, string sortOrder)
        {
            return _accommodationRepository.GetAccommodationsWithSearchSort(SearchValue, sortOrder);
        }

        public void Update(Accommodation accommodation)
        {
             _accommodationRepository.Update(accommodation);
        }
    }
}
