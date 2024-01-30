namespace ClientApp.Models;

public class UpdatesPeriod
{
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	public static List<UpdatesPeriod> Periods => new List<UpdatesPeriod>()
	{
		new UpdatesPeriod(){Id = 1, Name = "OnStartup"},
		new UpdatesPeriod(){Id = 2, Name = "EveryDay"},
		new UpdatesPeriod(){Id = 3, Name = "Never"},
		new UpdatesPeriod(){Id = 4, Name = "EveryDay"},
	};
}