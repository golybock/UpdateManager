using System.Configuration;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows;
using ClientApp.Models;

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
					Servers = ["https://localhost:7071/"],
					CurrentVersion = "1.0.0",
					AutoCheckUpdates = true,
					UpdatesPeriod = new UpdatesPeriod(){Id = 0, Name = "at start"},
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