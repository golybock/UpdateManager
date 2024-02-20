using System;
using System.Collections.Generic;

namespace TimeTableSite.Data;

public partial class Period
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}
