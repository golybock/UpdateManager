using UM.Models.Tasks.Database;
using UM.Models.Tasks.Domain;
using UM.Tools.Enums;

namespace UM.Models.Task.View;

public class WorkerView
{
	public Guid Id { get; set; }

	public Role Role { get; set; }

	public String FullName { get; set; }

	public String Login { get; set; }

	public WorkerView(WorkerDomain workerDomain)
	{
		Id = workerDomain.Id;
		Role = (Role) workerDomain.Role;
		FullName = workerDomain.FullName;
		Login = workerDomain.Login;
	}
}