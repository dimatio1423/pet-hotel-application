using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Role
{
    public string Id { get; set; }

    public string RoleName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
