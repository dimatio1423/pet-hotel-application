using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class ServiceImage
{
    public string Id { get; set; }

    public string Image { get; set; }

    public string ServiceId { get; set; }

    public virtual PetCareService Service { get; set; }
}
