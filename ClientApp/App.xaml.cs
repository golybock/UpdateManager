using System.IO;
using System.Text.Json;
using System.Windows;
using Desktop.Core.Models;

namespace ClientApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
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
					Servers = ["https://localhost:7071/;"],
					CurrentVersion = "1.0.0",
					UpdatesPeriod = UpdatesPeriod.Periods.ElementAt(1),
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