using System;
using System.Collections.Generic;

namespace TimeTableSite.Data;

public partial class Worker
{
    public Guid Id { get; set; }

    public int RoleId { get; set; }

    public string FullName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
