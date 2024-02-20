using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Desktop.Core.Api;
using SharedModels;
using TimetableManager.Windows;
using UM.Tools.Enums;
using UM.Tools.Extensions;

namespace TimetableManager.Pages;

public partial class SettingsPage : Page
{
	private readonly Settings _settings;
	private readonly List<string> _periods;
	private readonly MainWindow _mainWindow;

	public SettingsPage(MainWindow mainWindow)
	{
		_mainWindow = mainWindow;
		InitializeComponent();

		_settings = App.Settings;

		_periods = UpdatesPeriodExtension.GetAsStringEnumerable().ToList();

		InitSettingsPage();

		LoadVersion();
	}

	private void InitSettingsPage()
	{
		SaveArchiveCheckBox.IsChecked = _settings.SaveArchive;

		PeriodComboBox.ItemsSource = _periods;
		PeriodComboBox.SelectedValue = _periods.FirstOrDefault(c => c == _settings.UpdatesPeriod.GetDisplayName());

		ServersTextBox.Text = string.Join(";", _settings.Servers);
	}

	private async void LoadVersion()
	{
		try
		{
			VersionCard.Header += App.GetCurrentVersion();

			var api = new ApiUpdater(_settings.Servers);

			var versions = await api.GetAllVersions();

			VersionsItemsRepeater.ItemsSource = versions;

			var lastVersion = versions.MaxBy(c => c.Timestamp);

			VersionCard.Description += lastVersion?.Build;

			if (VersionToInt(lastVersion?.Build!) > VersionToInt(App.GetCurrentVersion()))
			{
				UpdateButton.Visibility = Visibility.Visible;
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);

			VersionCard.Description += "не удалось загрузить";
			VersionsItemsRepeater.Visibility = Visibility.Collapsed;

			PeriodComboBox.ItemsSource = _periods;
			PeriodComboBox.SelectedValue = _periods.ElementAt(0);

			_settings.UpdatesPeriod = UpdatesPeriod.Never;
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
		_settings.UpdatesPeriod = (UpdatesPeriod) Enum.Parse(typeof(UpdatesPeriod), PeriodComboBox.SelectedItem as string ?? string.Empty, true)!;
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
			var api = new ApiUpdater(_settings.Servers);

			var versions = await api.GetAllVersions();

			var lastVersion = versions.MaxBy(c => c.Timestamp);

			Process.Start("Installer.exe", lastVersion!.Id.ToString());

			Environment.Exit(0);
		}
		catch (Exception exception)
		{
			Console.WriteLine(exception);
			MessageBox.Show("Не удалось найти установочные файлы, требуется переустановка");
			// MessageBox.Show(exception.Message);
		}
	}

	private async void DownloadVersionButton_OnClick(object sender, RoutedEventArgs e)
	{
		try
		{
			var btn = sender as Button;

			var id = btn.CommandParameter is Guid guid ? guid : default;

			Process.Start("Installer.exe", id.ToString());

			Environment.Exit(0);
		}
		catch (Exception exception)
		{
			Console.WriteLine(exception);
			MessageBox.Show("Не удалось найти установочные файлы, требуется переустановка");
		}
	}

	private void CloseButton_OnClick(object sender, RoutedEventArgs e)
	{
		try
		{
			throw new Exception("Пример ошибки");
		}
		catch (Exception exception)
		{
			new ErrorWindow(exception.Message).Show();

			_mainWindow.Close();
		}
	}
}