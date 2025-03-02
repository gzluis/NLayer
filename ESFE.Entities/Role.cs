using System;
using System.Collections.Generic;

namespace ESFE.Entities;

public partial class Role
{
    public int RolId { get; set; }

    public string? RolName { get; set; }

    public bool? RolStatus { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
