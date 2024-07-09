using System;
using System.Collections.Generic;

namespace BusinessObjects.Models.BookingHistoryModel.Response
{
    public class BookingDetailsResModel
    {
        public string Id { get; set; }
        public string BoardingType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Note { get; set; }
        public AccommodationResModel Accommodation { get; set; }
        public PetResModel Pet { get; set; }
        public decimal TotalPrice { get; set; }
        public List<PetCareServiceResModel> PetCareServices { get; set; }
    }

    public class AccommodationResModel
    {
        public string Type { get; set; }
    }

    public class PetResModel
    {
        public string PetName { get; set; }
    }

    public class PetCareServiceResModel
    {
        public string Type { get; set; }
        public decimal Price { get; set; }
    }
}
