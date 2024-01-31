using UM.Models.Files.Blank;

namespace UM.Models.Files;

public class VersionDatabase
{
	public Guid Id { get; set; }

	public String Build { get; set; } = null!;

	public DateTime Timestamp { get; set; }

	public String Notes { get; set; } = null!;

	public Int32 Type { get; set; }

	public String Path { get; set; } = null!;

	public Boolean Available { get; set; }

	public VersionDatabase() { }

	public VersionDatabase(VersionBlank versionBlank)
	{
		Id = Guid.NewGuid();
		Build = versionBlank.Build;
		Timestamp = DateTime.UtcNow;
		Notes = versionBlank.Notes;
		Type = (int) versionBlank.Type;
		Path = versionBlank.Build + ".zip";
		Available = versionBlank.Available;
	}
}