using System;
using System.Collections.Generic;

namespace TimeTableSite.Data;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<TimetableRole> TimetableRoles { get; set; } = new List<TimetableRole>();
}
