using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Desktop.Core.Api;
using Desktop.Core.Models;

namespace Installer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
	private string _status;
	private int _progress = 0;

	private readonly ApiVersions _apiVersions;

	public string Status
	{
		get => _status;
		set
		{
			if (value == _status) return;
			_status = value;
			OnPropertyChanged();
		}
	}

	public int Progress
	{
		get => _progress;
		set
		{
			if (value == _progress) return;
			_progress = value;
			OnPropertyChanged();
		}
	}

	private readonly Settings _settings = App.Settings;

	public MainWindow()
	{
		InitializeComponent();

		DataContext = this;

		_status = "Начало установки";

		var args = Environment.GetCommandLineArgs();

		var versionId = Guid.Parse(args[1]);

		_apiVersions = new ApiVersions(_settings.Servers);

		Install(versionId);
	}

	private async void Install(Guid id)
	{
		Status = "Загрузка обновления";
		Progress += 40;

		await DownloadUpdate(id);

		Status = "Установка обновления";
		Progress += 40;

		ZipFile.ExtractToDirectory(Environment.CurrentDirectory + "/updates/" + id + ".zip", Environment.CurrentDirectory, true);

		if (!_settings.SaveArchive)
		{
			File.Delete(Environment.CurrentDirectory + "/updates/" + id + ".zip");
		}

		Progress += 20;
		Status = "Установлено";
		CloseButton.Visibility = Visibility.Visible;
	}

	private async Task DownloadUpdate(Guid id)
	{
		try
		{
			await _apiVersions.GetUpdateById(id);
		}
		catch (Exception e)
		{
			MessageBox.Show("Не удалось загрузить обновление");

			Process.Start(Environment.CurrentDirectory + "/TimeTableManager.exe");
		}
	}

	#region property change

	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}

	#endregion

	private void CloseButton_OnClick(object sender, RoutedEventArgs e)
	{
		Process.Start(Environment.CurrentDirectory + "/TimeTableManager.exe");

		Application.Current.Shutdown();
	}
}