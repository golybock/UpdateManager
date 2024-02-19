using System.Windows;
using System.Windows.Controls;
using TaskWorkerApp.Data;
using Task = System.Threading.Tasks.Task;

namespace TaskWorkerApp.Pages;

public partial class WorkerPage : Page
{
	private readonly Worker _worker;
	private readonly Frame _frame;
	private Data.Task? _task;

	private readonly UmFullContext _updatesContext = new UmFullContext();

	private DateTime _timer;

	public WorkerPage(Worker worker, Frame frame)
	{
		_worker = worker;
		_frame = frame;
		InitializeComponent();

		WorkerTextBlock.Text = worker.FullName;

		_task = _updatesContext.Tasks.OrderBy(c => c.Priority).FirstOrDefault(c => c.WorkerId == worker.Id && c.EndTime == null);

		if (_task == null)
		{
			NothingShowStackPanel.Visibility = Visibility.Visible;
			TaskStackPanel.Visibility = Visibility.Collapsed;
		}
		else
		{
			RenderProblem(_task);

			NothingShowStackPanel.Visibility = Visibility.Collapsed;
			TaskStackPanel.Visibility = Visibility.Visible;
		}
	}

	private void RenderProblem(Data.Task task)
	{
		TaskTextBlock.Text = task.Description;

		if (task.ClientEmail != null)
		{
			EmailTextBlock.Text = $"Почта клиента: {task.ClientEmail}";
		}

		StartTimer();
	}

	private async void StartTimer()
	{
		_timer = new DateTime(2000,1,1,1,0,0);

		TimerTextBlock.Text = $"Осталось времени: {_timer:hh:mm:ss}";

		while (true)
		{
			await Task.Delay(1000);

			_timer = _timer.AddSeconds(-1);

			if (_timer is {Minute: 0, Second: 0})
			{
				MessageBox.Show("Истекло время на решение, данные присвоены автоматически");
				_timer = new DateTime(2000,1,1,1,0,0);
			}

			TimerTextBlock.Text = $"Осталось времени: {_timer:hh:mm:ss}";
		}
	}

	private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
	{
		_task = _updatesContext.Tasks.FirstOrDefault(c => c.WorkerId == _worker.Id && c.EndTime == null);

		if (_task == null)
		{
			NothingShowStackPanel.Visibility = Visibility.Visible;
			TaskStackPanel.Visibility = Visibility.Collapsed;
		}
		else
		{
			RenderProblem(_task);

			NothingShowStackPanel.Visibility = Visibility.Collapsed;
			TaskStackPanel.Visibility = Visibility.Visible;
		}
	}

	private void NotWorkButton_OnClick(object sender, RoutedEventArgs e)
	{
		_task!.WorkerId = null;
		_task!.EndTime = null;
		_updatesContext.Tasks.Update(_task);
		_updatesContext.SaveChanges();

		var nextWork = _updatesContext.Tasks.FirstOrDefault(c => c.WorkerId == _worker.Id && c.EndTime == null);

		if (nextWork == null)
		{
			NothingShowStackPanel.Visibility = Visibility.Visible;
			TaskStackPanel.Visibility = Visibility.Collapsed;
		}
		else
		{
			RenderProblem(nextWork);

			NothingShowStackPanel.Visibility = Visibility.Collapsed;
			TaskStackPanel.Visibility = Visibility.Visible;
		}
	}

	private void MoreTime_OnClick(object sender, RoutedEventArgs e)
	{
		_timer = _timer.AddHours(1);
	}

	private void SendSolButton_OnClick(object sender, RoutedEventArgs e)
	{
		_frame.Navigate(new SolutionPage(_frame, _task!));

		_task.EndTime = DateTime.UtcNow;
		_task.Status = 5;

		_updatesContext.Tasks.Update(_task);
		_updatesContext.SaveChanges();

		_task = _updatesContext.Tasks.FirstOrDefault(c => c.WorkerId == _worker.Id && c.EndTime == null);

		if (_task == null)
		{
			NothingShowStackPanel.Visibility = Visibility.Visible;
			TaskStackPanel.Visibility = Visibility.Collapsed;
		}
		else
		{
			RenderProblem(_task);

			NothingShowStackPanel.Visibility = Visibility.Collapsed;
			TaskStackPanel.Visibility = Visibility.Visible;
		}
	}

	private void CloseButton_OnClick(object sender, RoutedEventArgs e)
	{
		_frame.NavigationService.Navigate(new LoginPage(_frame));
	}
}