using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Pet
{
    public string Id { get; set; }

    public string Avatar { get; set; }

    public string PetName { get; set; }

    public string Species { get; set; }

    public string Breed { get; set; }

    public int Age { get; set; }

    public string Status { get; set; }

    public string UserId { get; set; }

    public virtual ICollection<BookingInformation> BookingInformations { get; set; } = new List<BookingInformation>();

    public virtual User User { get; set; }
}
