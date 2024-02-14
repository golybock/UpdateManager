using UM.Models.Tasks.Blank;

namespace UM.Models.Tasks.Database;

public class TaskDatabase
{
	public Guid Id { get; set; }

	public String Description { get; set; } = null!;

	public Guid WorkerId { get; set; }

	public Int32 Priority { get; set; }

	public Int32 Status { get; set; }

	public DateTime CreatedTime { get; set; }

	public DateTime StartTime { get; set; }

	public DateTime EndTime { get; set; }

	public String? ClientEmail { get; set; }

	public String? Solution { get; set; }

	public TaskDatabase() { }

	public TaskDatabase(Guid id, TaskBlank taskBlank)
	{
		Id = id;
		Description = taskBlank.Description;
		WorkerId = taskBlank.WorkerId;
		Priority = (int) taskBlank.Priority;
		Status = (int) taskBlank.Status;
		StartTime = taskBlank.StartTime;
		EndTime = taskBlank.EndTime;
		ClientEmail = taskBlank.ClientEmail;
		Solution = taskBlank.Solution;
	}
}