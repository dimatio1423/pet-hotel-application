using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Accommodation
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int Capacity { get; set; }

    public string Status { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<BookingInformation> BookingInformations { get; set; } = new List<BookingInformation>();
}
