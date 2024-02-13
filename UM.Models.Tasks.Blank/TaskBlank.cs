using UM.Tools.Enums;

namespace UM.Models.Tasks.Blank;

public class TaskBlank
{
	public String Description { get; set; } = null!;

	public Guid WorkerId { get; set; }

	public Priority Priority { get; set; }

	public Status Status { get; set; }

	public DateTime StartTime { get; set; }

	public DateTime EndTime { get; set; }

	public String? ClientEmail { get; set; }

	public String? Solution { get; set; }
}