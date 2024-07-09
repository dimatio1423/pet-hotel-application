using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class PaymentRecord
{
    public string Id { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime Date { get; set; }

    public string Method { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public virtual BookingInformation Booking { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
