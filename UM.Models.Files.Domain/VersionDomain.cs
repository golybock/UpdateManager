using UM.Tools.Enums;

namespace UM.Models.Files.Domain;

public class VersionDomain
{
	public Guid Id { get; private set; }

	public String Build { get; private set; }

	public DateTime Timestamp { get; private set; }

	public String Notes { get; private set; }

	public UpdateType Type { get; private set; }

	public String Path { get; private set; }

	public IEnumerable<DependencyDomain> Dependencies { get; private set; }

	public VersionDomain(VersionDatabase versionDatabase, IEnumerable<DependencyDomain> dependencies)
	{
		Id = versionDatabase.Id;
		Build = versionDatabase.Build;
		Timestamp = versionDatabase.Timestamp;
		Notes = versionDatabase.Notes;
		Type = (UpdateType) versionDatabase.Type;
		Path = versionDatabase.Path;
		Dependencies = dependencies;
	}

	public VersionDomain(VersionDatabase versionDatabase)
	{
		Id = versionDatabase.Id;
		Build = versionDatabase.Build;
		Timestamp = versionDatabase.Timestamp;
		Notes = versionDatabase.Notes;
		Type = (UpdateType) versionDatabase.Type;
		Path = versionDatabase.Path;
		Dependencies = Enumerable.Empty<DependencyDomain>();
	}

	public VersionDomain(Guid id, string build, DateTime timestamp, string notes, int type, string path, IEnumerable<DependencyDomain> dependencies)
	{
		Id = id;
		Build = build;
		Timestamp = timestamp;
		Notes = notes;
		Type = (UpdateType) type;
		Path = path;
		Dependencies = dependencies;
	}
}