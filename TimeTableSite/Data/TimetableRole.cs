using System;
using System.Collections.Generic;

namespace TimeTableSite.Data;

public partial class TimetableRole
{
    public int Id { get; set; }

    public int TimetableId { get; set; }

    public int RoleId { get; set; }

    public decimal Count { get; set; }

    public decimal Salary { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual Timetable Timetable { get; set; } = null!;

    public virtual ICollection<TimetableRoleAllowance> TimetableRoleAllowances { get; set; } = new List<TimetableRoleAllowance>();
}
