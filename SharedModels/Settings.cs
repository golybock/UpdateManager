using UM.Tools.Enums;

namespace SharedModels;

public class Settings
{
	public List<string> Servers { get; set; } = new List<string>();

	public bool SaveArchive { get; set; }
	// and check updates always ____
	public UpdatesPeriod UpdatesPeriod { get; set; }

	public DateTime? LastCheckUpdate { get; set; }
}