using System.Windows;
using System.Windows.Controls;
using TaskWorkerApp.Data;
using UM.Tools.Enums;
using UM.Tools.Extensions;
using Task = System.Threading.Tasks.Task;

namespace TaskWorkerApp.Pages;

public partial class AdminPage : Page
{
	private List<Data.Task> _problems;
	private Data.Task? _currentProblem;

	private readonly Frame _frame;
	private readonly UmFullContext _context = new UmFullContext();
	public AdminPage(Frame frame)
	{
		_frame = frame;

		InitializeComponent();

		_problems = _context.Tasks.Where(c => c.WorkerId == null).ToList();

		if (_problems.Count == 0)
		{
			NothingShowPanel.Visibility = Visibility.Visible;
			TaskPanel.Visibility = Visibility.Collapsed;
		}
		else
		{
			NothingShowPanel.Visibility = Visibility.Collapsed;
			TaskPanel.Visibility = Visibility.Visible;
			_currentProblem = _problems.FirstOrDefault();
			ShowWork(_currentProblem!);
		}
	}

	private void ShowWork(Data.Task work)
	{
		PriorityComboBox.ItemsSource = PriorityExtension.GetAsStringEnumerable();

		var workers = _context.Workers.ToList();
		var admin = workers.FirstOrDefault(c => c.Login == "admin");
		workers.Remove(admin);

		WorkerComboBox.ItemsSource = workers;

		PriorityComboBox.SelectedIndex = 0;
		_currentProblem.Priority = (int)Enum.Parse(typeof(Priority), PriorityComboBox.SelectedValue.ToString());

		WorkerComboBox.SelectedIndex = 0;
		_currentProblem.WorkerId = (WorkerComboBox.SelectedValue as Worker).Id;

		TaskTextBlock.Text = work.Description;

		if (work.ClientEmail != null)
		{
			EmailTextBlock.Text = $"Почта клиента: {work.ClientEmail}";
		}

		StartTimer();
	}

	private async void StartTimer()
	{
		var startTime = new DateTime(2000,1,1,1,5,0);

		TimerTextBlock.Text = $"Осталось времени: {startTime:mm:ss}";

		while (true)
		{
			await Task.Delay(1000);

			startTime = startTime.AddSeconds(-1);

			if (startTime is {Minute: 0, Second: 0})
			{
				MessageBox.Show("Истекло время на решение, данные присвоены автоматически");

				_currentProblem.Status = 3;
				_currentProblem.StartTime = DateTime.UtcNow;

				_context.Tasks.Update(_currentProblem);
				await _context.SaveChangesAsync();

				if (_currentProblem.ClientEmail != null)
				{
					await EmailSender.SendEmailAsync(_currentProblem!.ClientEmail, "Назначен исполнитель");
				}

				_problems.RemoveAt(0);

				if (_problems.Count == 0)
				{
					NothingShowPanel.Visibility = Visibility.Visible;
					TaskPanel.Visibility = Visibility.Collapsed;
					return;
				}

				_currentProblem = _problems.FirstOrDefault();
				ShowWork(_currentProblem!);
			}

			TimerTextBlock.Text = $"Осталось времени: {startTime:mm:ss}";
		}
	}

	private void PriorityComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		_currentProblem.Priority = (int)Enum.Parse(typeof(Priority), PriorityComboBox.SelectedValue.ToString());
	}

	private void WorkerComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		_currentProblem.WorkerId = (WorkerComboBox.SelectedValue as Worker).Id;
	}

	private async void SaveButton_OnClick(object sender, RoutedEventArgs e)
	{
		_currentProblem.Status = 3;
		_currentProblem.StartTime = DateTime.UtcNow;

		_context.Tasks.Update(_currentProblem);
		await _context.SaveChangesAsync();

		_problems.RemoveAt(0);

		MessageBox.Show("Сохранено!");

		if (_problems.Count == 0)
		{
			NothingShowPanel.Visibility = Visibility.Visible;
			TaskPanel.Visibility = Visibility.Collapsed;
			return;
		}

		if (_currentProblem!.ClientEmail != null)
		{
			await EmailSender.SendEmailAsync(_currentProblem!.ClientEmail, "Назначен исполнитель");
		}

		_currentProblem = _problems.FirstOrDefault();
		ShowWork(_currentProblem!);
	}

	private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
	{
		_problems = _context.Tasks.Where(c => c.WorkerId == null).ToList();

		if (_problems.Count == 0)
		{
			NothingShowPanel.Visibility = Visibility.Visible;
			TaskPanel.Visibility = Visibility.Collapsed;
		}
		else
		{
			NothingShowPanel.Visibility = Visibility.Collapsed;
			TaskPanel.Visibility = Visibility.Visible;

			_currentProblem = _problems.FirstOrDefault();
			ShowWork(_currentProblem!);
		}
	}

	private void CloseButton_OnClick(object sender, RoutedEventArgs e)
	{
		_frame.NavigationService.Navigate(new LoginPage(_frame));
	}
}