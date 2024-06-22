using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class User
{
    public string Id { get; set; }

    public string Avatar { get; set; }

    public string FullName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Address { get; set; }

    public string Status { get; set; }

    public string RoleId { get; set; }

    public virtual ICollection<BookingInformation> BookingInformations { get; set; } = new List<BookingInformation>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<PaymentRecord> PaymentRecords { get; set; } = new List<PaymentRecord>();

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();

    public virtual Role Role { get; set; }
}
