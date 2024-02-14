using UM.Models.Tasks.Domain;
using UM.Tools.Enums;

namespace UM.Models.Task.View;

public class TaskView
{
	public Guid Id { get; set; }

	public String Description { get; set; } = null!;

	public WorkerView? Worker { get; set; }

	public Priority Priority { get; set; }

	public Status Status { get; set; }

	public DateTime CreatedTime { get; set; }

	public DateTime StartTime { get; set; }

	public DateTime EndTime { get; set; }

	public String? ClientEmail { get; set; }

	public String? Solution { get; set; }

	public TaskView(TaskDomain taskDomain)
	{
		Id = taskDomain.Id;
		Worker = taskDomain.Worker != null ? new WorkerView(taskDomain.Worker) : null;
		Priority = (Priority) taskDomain.Priority;
		Status = (Status) taskDomain.Status;
		CreatedTime = taskDomain.CreatedTime;
		StartTime = taskDomain.StartTime;
		EndTime = taskDomain.EndTime;
		ClientEmail = taskDomain.ClientEmail;
		Solution = taskDomain.Solution;
	}
}