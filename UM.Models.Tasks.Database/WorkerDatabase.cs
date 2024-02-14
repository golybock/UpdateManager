using UM.Models.Tasks.Blank;

namespace UM.Models.Tasks.Database;

public class WorkerDatabase
{
	public Guid Id { get; set; }

	public Int32 RoleId { get; set; }

	public String FullName { get; set; } = null!;

	public String Login { get; set; } = null!;

	public Byte[] Password { get; set; } = null!;

	public WorkerDatabase() { }

	public WorkerDatabase(WorkerBlank workerBlank)
	{
		Id = Guid.NewGuid();
		RoleId = workerBlank.RoleId;
		FullName = workerBlank.FullName;
		Login = workerBlank.Login;
	}
}