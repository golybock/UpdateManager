using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using SharedModels;
using UM.Tools.Enums;

namespace TimetableManager;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	public static string GetCurrentVersion()
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
		return versionInfo.FileVersion!;
	}

	public static Settings Settings
	{
		get
		{
			try
			{
				string res = File.ReadAllText("settings.json");

				var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};

				return JsonSerializer.Deserialize<Settings>(res,options)!;
			}
			catch (Exception e)
			{
				Settings settings = new Settings()
				{
					Servers = ["https://localhost:7079;"],
					UpdatesPeriod = UpdatesPeriod.OnStartup,
					SaveArchive = true
				};

				SaveSettings(settings);

				return settings;
			}
		}
	}

	public static void SaveSettings(Settings settings)
	{
		var res = JsonSerializer.Serialize(settings);
		File.WriteAllText("settings.json", res);
	}
}