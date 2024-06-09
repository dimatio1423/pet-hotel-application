using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class ServiceBooking
{
    public string Id { get; set; }

    public string ServiceId { get; set; }

    public string BookingId { get; set; }

    public virtual BookingInformation Booking { get; set; }

    public virtual PetCareService Service { get; set; }
}
