using UM.Models.Files.Domain;
using UM.Tools.Enums;

namespace UM.Models.Files.View;

public class VersionView
{
	public Guid Id { get; set; }

	public String Build { get; set; }

	public DateTime Timestamp { get; set; }

	public String Notes { get; set; }

	public UpdateType Type { get; set; }

	public IEnumerable<DependencyView> Dependencies { get; set; }

	public VersionView() { }

	public VersionView(VersionDomain versionDatabase, IEnumerable<DependencyView> dependencies)
	{
		Id = versionDatabase.Id;
		Build = versionDatabase.Build;
		Timestamp = versionDatabase.Timestamp;
		Notes = versionDatabase.Notes;
		Type = versionDatabase.Type;
		Dependencies = dependencies;
	}

	public VersionView(VersionDomain versionDatabase)
	{
		Id = versionDatabase.Id;
		Build = versionDatabase.Build;
		Timestamp = versionDatabase.Timestamp;
		Notes = versionDatabase.Notes;
		Type = versionDatabase.Type;
		Dependencies = Enumerable.Empty<DependencyView>();
	}

	public VersionView(Guid id, string build, DateTime timestamp, string notes, UpdateType type, string path, IEnumerable<DependencyView> dependencies)
	{
		Id = id;
		Build = build;
		Timestamp = timestamp;
		Notes = notes;
		Type = type;
		Dependencies = dependencies;
	}
}