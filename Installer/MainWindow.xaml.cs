using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Windows;
using Desktop.Core.Api;
using SharedModels;

namespace Installer;

public partial class MainWindow : Window, INotifyPropertyChanged
{
	private readonly Settings _settings;
	private readonly ApiUpdater _apiUpdater;

	private string _status = "Инициализация";
	private int _progress = 0;

	public String Status
	{
		get => _status;
		set
		{
			if (value == _status) return;
			_status = value;
			OnPropertyChanged();
		}
	}

	public Int32 Progress
	{
		get => _progress;
		set
		{
			if (value == _progress) return;
			_progress = value;
			OnPropertyChanged();
		}
	}

	public MainWindow()
	{
		InitializeComponent();

		DataContext = this;

		_settings = App.Settings;
		_apiUpdater = new ApiUpdater(_settings.Servers);

		var args = Environment.GetCommandLineArgs();

		var versionId = Guid.Parse(args[1]);

		Install(versionId);
	}

	private async void Install(Guid id)
	{
		try
		{
			_status = "Начало установки";

			await DownloadUpdateAsync(id);

			ExtractFiles(id);

			FinalInstall();
		}
		catch (Exception e)
		{
			MessageBox.Show(e.Message);

			CloseInstaller();
		}
	}

	private async Task DownloadUpdateAsync(Guid id)
	{
		Status = "Загрузка обновления";

		await DownloadFiles(id);
		Progress += 40;
	}

	private void ExtractFiles(Guid id)
	{
		Status = "Установка обновления";

		ZipFile.ExtractToDirectory(Environment.CurrentDirectory + "/updates/" + id + ".zip", Environment.CurrentDirectory, true);

		DeleteArchive(id);
		Progress += 40;
	}

	private void FinalInstall()
	{
		Progress = 100;
		Status = "Установлено";
		CloseButton.Visibility = Visibility.Visible;
	}

	private void DeleteArchive(Guid id)
	{
		if (!_settings.SaveArchive)
		{
			File.Delete(Environment.CurrentDirectory + "/updates/" + id + ".zip");
		}
	}

	private async Task DownloadFiles(Guid id)
	{
		try
		{
			await _apiUpdater.GetUpdateById(id);
		}
		catch (Exception e)
		{
			// throw new Exception("Не удалось загрузить обновление");
			throw new Exception(e.Message);
		}
	}

	private void CloseButton_OnClick(object sender, RoutedEventArgs e)
	{
		CloseInstaller();
	}

	private static void CloseInstaller()
	{
		Process.Start(Environment.CurrentDirectory + "/TimeTableManager.exe");

		Application.Current.Shutdown();
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
}