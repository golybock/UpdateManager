namespace UM.Models.Tasks.Blank;

public class WorkerBlank
{
	public Int32 RoleId { get; set; }

	public String FullName { get; set; } = null!;

	public String Login { get; set; } = null!;

	public String Password { get; set; } = null!;
}