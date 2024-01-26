namespace UM.Models.Files;

public class DependencyDatabase
{
	public Guid Id { get; set; }

	public String Name { get; set; } = null!;

	public String Version { get; set; } = null!;
}