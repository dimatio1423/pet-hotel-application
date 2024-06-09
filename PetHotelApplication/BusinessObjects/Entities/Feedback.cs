using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Feedback
{
    public string Id { get; set; }

    public string Comment { get; set; }

    public int Rating { get; set; }

    public DateTime Date { get; set; }

    public string UserId { get; set; }

    public virtual User User { get; set; }
}
