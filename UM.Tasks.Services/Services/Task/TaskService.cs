using UM.Models.Task.View;
using UM.Models.Tasks.Blank;
using UM.Models.Tasks.Database;
using UM.Models.Tasks.Domain;
using UM.Tasks.Repositories.Repositories.Task;
using UM.Tasks.Repositories.Repositories.Worker;

namespace UM.Tasks.Services.Services.Task;

public class TaskService : ITaskService
{
	private readonly ITaskRepository _taskRepository;
	private readonly IWorkerRepository _workerRepository;

	public TaskService(ITaskRepository taskRepository, IWorkerRepository workerRepository)
	{
		_taskRepository = taskRepository;
		_workerRepository = workerRepository;
	}

	public async Task<IEnumerable<TaskView>> GetTasksAsync()
	{
		var tasks = await _taskRepository.GetTasksAsync();
		var workers = await _workerRepository.GetWorkersAsync();

		var tasksDomain = tasks
			.Select(t =>
			{
				var worker = workers.FirstOrDefault(c => t.WorkerId == c.Id);

				return new TaskDomain(t, worker);
			})
			.ToList();

		var tasksView = tasksDomain
			.Select(t => new TaskView(t))
			.ToList();

		return tasksView;
	}

	public async Task<IEnumerable<TaskView>> GetEmptyTasksAsync()
	{
		var tasks = await _taskRepository.GetEmptyTasksAsync();
		var workers = await _workerRepository.GetWorkersAsync();

		var tasksDomain = tasks
			.Select(t =>
			{
				var worker = workers.FirstOrDefault(c => t.WorkerId == c.Id);

				return new TaskDomain(t, worker);
			})
			.ToList();

		var tasksView = tasksDomain
			.Select(t => new TaskView(t))
			.ToList();

		return tasksView;
	}

	public async Task<TaskView?> GetTaskAsync(Guid id)
	{
		var task = await _taskRepository.GetTaskAsync(id);

		if (task == null)
			return null;

		var worker = await _workerRepository.GetWorkerAsync(task.WorkerId);

		var taskDomain = new TaskDomain(task, worker);

		var taskView = new TaskView(taskDomain);

		return taskView;
	}

	public async Task<bool> CreateTaskAsync(TaskBlank task)
	{
		var taskDatabase = new TaskDatabase(Guid.NewGuid(), task);

		var result = await _taskRepository.CreateTaskAsync(taskDatabase);

		return result;
	}

	public async Task<bool> UpdateTaskAsync(Guid id, TaskBlank task)
	{
		var taskDatabase = new TaskDatabase(id, task);

		var result = await _taskRepository.UpdateTaskAsync(taskDatabase);

		return result;
	}

	public async Task<bool> DeleteTaskAsync(Guid id)
	{
		var result = await _taskRepository.DeleteTaskAsync(id);

		return result;
	}
}