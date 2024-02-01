using System.Configuration;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows;
using Desktop.Core.Models;

namespace Installer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	public static Settings Settings
	{
		get
		{
			string res = File.ReadAllText("settings.json");

			var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};

			return JsonSerializer.Deserialize<Settings>(res,options)!;
		}
	}
}