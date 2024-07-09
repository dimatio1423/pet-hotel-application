using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class ServiceBooking
{
    public string Id { get; set; } = null!;

    public string ServiceId { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public virtual BookingInformation Booking { get; set; } = null!;

    public virtual PetCareService Service { get; set; } = null!;
}
