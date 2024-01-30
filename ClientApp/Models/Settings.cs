namespace ClientApp.Models;

public class Settings
{
	public bool SaveArchive { get; set; }

	public List<string> Servers { get; set; } = new List<string>();

	public bool AutoCheckUpdates { get; set; }

	public UpdatesPeriod UpdatesPeriod { get; set; } = null!;

	public string CurrentVersion { get; set; } = null!;

	public string? VersionToInstall { get; set; }

	public DateTime LastCheckUpdate { get; set; }
}