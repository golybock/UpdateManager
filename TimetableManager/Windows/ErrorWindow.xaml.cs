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

	private async void RestartButton_OnClick(object sender, RoutedEventArgs e)
	{
		if (EmailTextBox.Text != string.Empty)
		{
			if (!new EmailAddressAttribute().IsValid(EmailTextBox.Text))
			{
				MessageBox.Show("Неверный формат почты");
				return;
			}

			await ExceptionMailSender.SendEmailAsync(error, EmailTextBox.Text);
		}

		new MainWindow().Show();

		Close();
	}

	private async void CloseButton_OnClick(object sender, RoutedEventArgs e)
	{
		if (EmailTextBox.Text != string.Empty)
		{
			if (!new EmailAddressAttribute().IsValid(EmailTextBox.Text))
			{
				MessageBox.Show("Неверный формат почты");
				return;
			}

			await ExceptionMailSender.SendEmailAsync(error, EmailTextBox.Text);
		}

		Close();
	}
}