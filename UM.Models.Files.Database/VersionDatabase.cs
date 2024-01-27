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
}