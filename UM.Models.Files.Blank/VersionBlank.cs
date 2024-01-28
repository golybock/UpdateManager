using UM.Tools.Enums;

namespace UM.Models.Files.Blank;

public class VersionBlank
{
	public String Build { get; set; } = null!;

	public DateTime Timestamp { get; set; }

	public String Notes { get; set; } = null!;

	public UpdateType Type { get; set; }

	public String Path { get; set; } = null!;

	public Boolean Available { get; set; }
}