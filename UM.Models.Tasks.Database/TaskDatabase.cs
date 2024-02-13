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
}