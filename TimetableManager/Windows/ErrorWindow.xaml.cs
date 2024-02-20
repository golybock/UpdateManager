using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace TimetableManager.Windows;

public partial class ErrorWindow : Window
{
	private string error;

	public ErrorWindow(string error)
	{
		this.error = error;
		InitializeComponent();
	}

	private void RestartButton_OnClick(object sender, RoutedEventArgs e)
	{
		if (EmailTextBox.Text != string.Empty)
		{
			if (!new EmailAddressAttribute().IsValid(EmailTextBox.Text))
			{
				MessageBox.Show("Неверный формат почты");
				return;
			}
		}
	}

	private void CloseButton_OnClick(object sender, RoutedEventArgs e)
	{
		throw new NotImplementedException();
	}
}