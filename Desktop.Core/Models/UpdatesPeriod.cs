namespace Desktop.Core.Models;

// todo можно сделать как enum
public class UpdatesPeriod
{
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	public static List<UpdatesPeriod> Periods =>
	[
		new UpdatesPeriod() {Id = -1, Name = "Never"},
		new UpdatesPeriod() {Id = 1, Name = "OnStartup"},
		new UpdatesPeriod() {Id = 2, Name = "EveryDay"},
		new UpdatesPeriod() {Id = 3, Name = "EveryWeek"}
	];
}