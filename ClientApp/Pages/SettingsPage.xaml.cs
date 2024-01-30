using System.Windows;
using System.Windows.Controls;
using ClientApp.Models;

namespace ClientApp.Pages;

public partial class SettingsPage : Page
{
	public Settings Settings => App.Settings;

	public SettingsPage()
	{
		InitializeComponent();
	}

	private void SaveArchiveCheckBox_OnChecked(object sender, RoutedEventArgs e)
	{
		Settings.SaveArchive = true;
		App.SaveSettings(Settings);
	}

	private void SaveArchiveCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
	{
		Settings.SaveArchive = true;
		App.SaveSettings(Settings);
	}

	private void PeriodComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{

	}
}