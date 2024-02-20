using System;
using System.Collections.Generic;

namespace TimeTableSite.Data;

public partial class Allowance
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }

    public virtual ICollection<TimetableRoleAllowance> TimetableRoleAllowances { get; set; } = new List<TimetableRoleAllowance>();
}
