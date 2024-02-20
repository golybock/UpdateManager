using System;
using System.Collections.Generic;

namespace TimeTableSite.Data;

public partial class TimetableRoleAllowance
{
    public int Id { get; set; }

    public int TimetableRoleId { get; set; }

    public int AllowanceId { get; set; }

    public virtual Allowance Allowance { get; set; } = null!;

    public virtual TimetableRole TimetableRole { get; set; } = null!;
}
