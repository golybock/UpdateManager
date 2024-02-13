using UM.Models.Tasks.Database;
using UM.Tools.Enums;

namespace UM.Models.Tasks.Domain;

public class WorkerDomain
{
	public Guid Id { get; set; }

	public Role Role { get; set; }

	public String FullName { get; set; }

	public String Login { get; set; }

	public WorkerDomain(WorkerDatabase workerDatabase)
	{
		Id = workerDatabase.Id;
		Role = (Role) workerDatabase.RoleId;
		FullName = workerDatabase.FullName;
		Login = workerDatabase.Login;
	}
}