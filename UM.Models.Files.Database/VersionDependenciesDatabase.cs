namespace UM.Models.Files;

public class VersionDependenciesDatabase
{
	public Int32 Id { get; set; }

	public Guid VersionId { get; set; }

	public Guid DependencyId { get; set; }
}