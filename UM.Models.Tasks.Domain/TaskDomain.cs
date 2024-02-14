using UM.Models.Tasks.Database;
using UM.Tools.Enums;

namespace UM.Models.Tasks.Domain;

public class TaskDomain
{
	public Guid Id { get; set; }

	public String Description { get; set; } = null!;

	public WorkerDomain? Worker { get; set; }

	public Priority Priority { get; set; }

	public Status Status { get; set; }

	public DateTime CreatedTime { get; set; }

	public DateTime StartTime { get; set; }

	public DateTime EndTime { get; set; }

	public String? ClientEmail { get; set; }

	public String? Solution { get; set; }

	public TaskDomain(TaskDatabase taskDatabase, WorkerDatabase? workerDatabase)
	{
		Id = taskDatabase.Id;
		Worker = workerDatabase == null ? null : new WorkerDomain(workerDatabase);
		Priority = (Priority) taskDatabase.Priority;
		Status = (Status) taskDatabase.Status;
		CreatedTime = taskDatabase.CreatedTime;
		StartTime = taskDatabase.StartTime;
		EndTime = taskDatabase.EndTime;
		ClientEmail = taskDatabase.ClientEmail;
		Solution = taskDatabase.Solution;
	}

	public TaskDomain(TaskDatabase taskDatabase, WorkerDomain? workerDomain)
	{
		Id = taskDatabase.Id;
		Worker = workerDomain;
		Priority = (Priority) taskDatabase.Priority;
		Status = (Status) taskDatabase.Status;
		CreatedTime = taskDatabase.CreatedTime;
		StartTime = taskDatabase.StartTime;
		EndTime = taskDatabase.EndTime;
		ClientEmail = taskDatabase.ClientEmail;
		Solution = taskDatabase.Solution;
	}
}