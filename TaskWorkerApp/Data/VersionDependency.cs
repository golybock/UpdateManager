using System;
using System.Collections.Generic;

namespace TaskWorkerApp.Data;

public partial class VersionDependency
{
    public int Id { get; set; }

    public Guid VersionId { get; set; }

    public Guid DependencyId { get; set; }

    public virtual Dependency Dependency { get; set; } = null!;

    public virtual Version Version { get; set; } = null!;
}
