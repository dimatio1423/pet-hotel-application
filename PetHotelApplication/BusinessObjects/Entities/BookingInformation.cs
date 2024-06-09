using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class BookingInformation
{
    public string Id { get; set; }

    public string BoardingType { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Note { get; set; }

    public string FeedingSchedule { get; set; }

    public string ExerciseRoutine { get; set; }

    public string Status { get; set; }

    public string UserId { get; set; }

    public string AccommodationId { get; set; }

    public string PetId { get; set; }

    public virtual Accommodation Accommodation { get; set; }

    public virtual ICollection<PaymentRecord> PaymentRecords { get; set; } = new List<PaymentRecord>();

    public virtual Pet Pet { get; set; }

    public virtual ICollection<ServiceBooking> ServiceBookings { get; set; } = new List<ServiceBooking>();

    public virtual User User { get; set; }
}
