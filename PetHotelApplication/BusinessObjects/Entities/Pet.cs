using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Pet
{
    public string Id { get; set; } = null!;

    public string? Avatar { get; set; }

    public string PetName { get; set; } = null!;

    public string Species { get; set; } = null!;

    public string Breed { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Status { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual ICollection<BookingInformation> BookingInformations { get; set; } = new List<BookingInformation>();

    public virtual User User { get; set; } = null!;
}
