using UM.Tools.Enums;

namespace UM.Models.Files.Blank;

public class VersionBlank
{
	public String Build { get; set; } = null!;

	public String Notes { get; set; } = null!;

	public UpdateType Type { get; set; }

	public Boolean Available { get; set; }
}