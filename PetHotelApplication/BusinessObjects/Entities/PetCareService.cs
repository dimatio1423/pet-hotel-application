using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class PetCareService
{
    public string Id { get; set; }

    public string Type { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<ServiceBooking> ServiceBookings { get; set; } = new List<ServiceBooking>();
}
