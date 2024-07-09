using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Feedback
{
    public string Id { get; set; } = null!;

    public string? Comment { get; set; }

    public int Rating { get; set; }

    public DateTime Date { get; set; }

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
