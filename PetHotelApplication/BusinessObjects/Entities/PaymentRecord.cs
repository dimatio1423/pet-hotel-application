using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class PaymentRecord
{
    public string Id { get; set; }

    public decimal Price { get; set; }

    public DateTime Date { get; set; }

    public string Method { get; set; }

    public string Status { get; set; }

    public string UserId { get; set; }

    public string BookingId { get; set; }

    public virtual BookingInformation Booking { get; set; }

    public virtual User User { get; set; }
}
