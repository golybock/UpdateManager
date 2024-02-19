using System;
using System.Collections.Generic;

namespace UM.API.Data;

public partial class Version
{
    public Guid Id { get; set; }

    public string Build { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string Notes { get; set; } = null!;

    public int Type { get; set; }

    public string Path { get; set; } = null!;

    public bool Available { get; set; }

    public virtual ICollection<VersionDependency> VersionDependencies { get; set; } = new List<VersionDependency>();
}
