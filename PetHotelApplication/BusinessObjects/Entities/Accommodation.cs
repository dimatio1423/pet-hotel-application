using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Accommodation
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public int Capacity { get; set; }

    public string Status { get; set; }

    public string Description { get; set; }

    public virtual ICollection<BookingInformation> BookingInformations { get; set; } = new List<BookingInformation>();
}
