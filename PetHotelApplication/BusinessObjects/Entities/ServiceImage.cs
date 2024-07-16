using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class ServiceImage
{
    public string Id { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string ServiceId { get; set; } = null!;

    public virtual PetCareService Service { get; set; } = null!;
}
