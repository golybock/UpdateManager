using UM.Models.Task.View;
using UM.Models.Tasks.Blank;

namespace UM.Tasks.Services.Services.Task;

public interface ITaskService
{
	public Task<IEnumerable<TaskView>> GetTasksAsync();

	// Get all tasks that are not assigned to any user
	public Task<IEnumerable<TaskView>> GetEmptyTasksAsync();

	public Task<TaskView?> GetTaskAsync(Guid id);

	public Task<Boolean> CreateTaskAsync(TaskBlank task);

	public Task<Boolean> UpdateTaskAsync(Guid id, TaskBlank task);

	public Task<Boolean> DeleteTaskAsync(Guid id);
}