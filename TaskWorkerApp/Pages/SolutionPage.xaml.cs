using System.Windows;
using System.Windows.Controls;
using TaskWorkerApp.Data;
using Task = TaskWorkerApp.Data.Task;

namespace TaskWorkerApp.Pages;

public partial class SolutionPage : Page
{
	private readonly Frame _frame;
	private readonly Task _problem;
	private readonly UmFullContext _context = new UmFullContext();
	public SolutionPage(Frame frame, Task problem)
	{
		_frame = frame;
		_problem = problem;

		InitializeComponent();
	}

	private async void SendButton_OnClick(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(VersionTextBox.Text))
		{
			MessageBox.Show("Укажите версию!");
			return;
		}

		if (string.IsNullOrWhiteSpace(SolutionTextBox.Text))
		{
			MessageBox.Show("Укажите решение!");
			return;
		}

		_problem.Solution = SolutionTextBox.Text;
		_problem.Version = VersionTextBox.Text;
		_context.Tasks.Update(_problem);

		await _context.SaveChangesAsync();

		if (_problem.ClientEmail == null)
		{
			MessageBox.Show("Задача выполнена");
			_frame.GoBack();
			return;
		}

		await EmailSender.SendEmailAsync(_problem.ClientEmail, SolutionTextBox.Text);

		MessageBox.Show("Отправлено!");
		_frame.GoBack();
	}
}