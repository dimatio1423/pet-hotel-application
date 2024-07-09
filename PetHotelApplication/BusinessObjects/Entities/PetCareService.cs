using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class PetCareService
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<ServiceBooking> ServiceBookings { get; set; } = new List<ServiceBooking>();

    public virtual ICollection<ServiceImage> ServiceImages { get; set; } = new List<ServiceImage>();
}
