using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Desktop.Core.Api;
using Desktop.Core.Models;

namespace ClientApp.Pages;

public partial class SettingsPage : Page
{
	private readonly Settings _settings;

	public SettingsPage()
	{
		InitializeComponent();

		_settings = App.Settings;

		SaveArchiveCheckBox.IsChecked = _settings.SaveArchive;

		var periods = UpdatesPeriod.Periods;

		PeriodComboBox.ItemsSource = periods;
		PeriodComboBox.SelectedValue = periods.FirstOrDefault(c => c.Id == _settings.UpdatesPeriod.Id);

		ServersTextBox.Text = string.Join(";", _settings.Servers);

		LoadVersion();
	}

	private async void LoadVersion()
	{
		try
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

			VersionCard.Header += fvi.FileVersion;

			var api = new ApiVersions(_settings.Servers);

			var versions = await api.GetAllVersions();

			VersionsItemsRepeater.ItemsSource = versions;

			var lastVersion = versions.MaxBy(c => c.Timestamp);

			VersionCard.Description += lastVersion?.Build;

			if (VersionToInt(lastVersion?.Build!) > VersionToInt(fvi.FileVersion!))
			{
				UpdateButton.Visibility = Visibility.Visible;
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);

			var periods = UpdatesPeriod.Periods;

			VersionCard.Description += "не удалось загрузить";
			VersionsItemsRepeater.Visibility = Visibility.Collapsed;

			PeriodComboBox.ItemsSource = periods;
			PeriodComboBox.SelectedValue = periods.ElementAt(0);

			_settings.UpdatesPeriod = periods.ElementAt(0);
			App.SaveSettings(_settings);

			MessageBox.Show("Не удалось получить данные с сервера, проверьте адрес или подключение к интернету");
		}
	}

	private int VersionToInt(string version)
	{
		var nums = version.Select(c =>
		{
			if (char.IsNumber(c))
			{
				return c;
			}

			return 0;
		});

		return nums.Sum();
	}

	private void SaveArchiveCheckBox_OnChecked(object sender, RoutedEventArgs e)
	{
		_settings.SaveArchive = true;
		App.SaveSettings(_settings);
	}

	private void SaveArchiveCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
	{
		_settings.SaveArchive = false;
		App.SaveSettings(_settings);
	}

	private void PeriodComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		_settings.UpdatesPeriod = (PeriodComboBox.SelectedItem as UpdatesPeriod)!;
		App.SaveSettings(_settings);
	}

	private void ServersTextBox_OnKeyDown(object sender, KeyEventArgs e)
	{
		// save
		if (e.Key == Key.Enter)
		{
			var servers = ServersTextBox.Text;

			var urls = ParseUrls(servers);

			_settings.Servers = urls;
			App.SaveSettings(_settings);

			ServersTextBox.Text = string.Join(";", urls);
		}
	}

	private List<string> ParseUrls(string servers)
	{
		List<string> result = new List<string>();

		var urls = servers.Split(";");

		foreach (var url in urls)
		{
			bool isValid = Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

			if (isValid)
			{
				result.Add(uriResult!.ToString());
			}
		}

		return result;
	}

	private async void UpdateButton_OnClick(object sender, RoutedEventArgs e)
	{
		try
		{
			var api = new ApiVersions(_settings.Servers);

			var versions = await api.GetAllVersions();

			var lastVersion = versions.MaxBy(c => c.Timestamp);

			Process.Start("Installer.exe", lastVersion!.Id.ToString());
		}
		catch (Exception exception)
		{
			MessageBox.Show("Не удалось найти установочные файлы");
		}
	}
}