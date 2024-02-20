using System;
using System.Collections.Generic;

namespace TimeTableSite.Data;

public partial class Timetable
{
    public int Id { get; set; }

    public int DocumentNumber { get; set; }

    public DateTime CreatedDate { get; set; }

    public string ChiefAccountant { get; set; } = null!;

    public int PeriodId { get; set; }

    public int SupervisorId { get; set; }

    public virtual Period Period { get; set; } = null!;

    public virtual Supervisor Supervisor { get; set; } = null!;

    public virtual ICollection<TimetableRole> TimetableRoles { get; set; } = new List<TimetableRole>();
}
