using System.Windows;
using ModernWpf.Controls;
using TimetableManager.Pages;

namespace ClientApp.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void NavigationView_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
	{
		if (args.IsSettingsInvoked)
		{
			MainFrame.Navigate(new SettingsPage());
		}
	}
}