using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? Avatar { get; set; }

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public virtual ICollection<BookingInformation> BookingInformations { get; set; } = new List<BookingInformation>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<PaymentRecord> PaymentRecords { get; set; } = new List<PaymentRecord>();

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();

    public virtual Role Role { get; set; } = null!;
}
