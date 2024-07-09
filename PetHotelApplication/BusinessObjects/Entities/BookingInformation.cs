using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class BookingInformation
{
    public string Id { get; set; } = null!;

    public string BoardingType { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Note { get; set; }

    public string? Status { get; set; }

    public string UserId { get; set; } = null!;

    public string AccommodationId { get; set; } = null!;

    public string PetId { get; set; } = null!;

    public virtual Accommodation Accommodation { get; set; } = null!;

    public virtual ICollection<PaymentRecord> PaymentRecords { get; set; } = new List<PaymentRecord>();

    public virtual Pet Pet { get; set; } = null!;

    public virtual ICollection<ServiceBooking> ServiceBookings { get; set; } = new List<ServiceBooking>();

    public virtual User User { get; set; } = null!;
}
