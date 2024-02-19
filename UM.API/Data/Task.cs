using System;
using System.Collections.Generic;

namespace UM.API.Data;

public partial class Task
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public Guid? WorkerId { get; set; }

    public int? Priority { get; set; }

    public int? Status { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? ClientEmail { get; set; }

    public string? Solution { get; set; }

    public virtual Worker? Worker { get; set; }
}
