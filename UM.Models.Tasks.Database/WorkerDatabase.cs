namespace UM.Models.Tasks.Database;

public class WorkerDatabase
{
	public Guid Id { get; set; }

	public Int32 RoleId { get; set; }

	public String FullName { get; set; } = null!;

	public String Login { get; set; } = null!;

	public required Byte[] Password { get; set; }
}