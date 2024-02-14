using UM.Models.Tasks.Database;

namespace UM.Tasks.Repositories.Repositories.Task;

public interface ITaskRepository
{
	public Task<IEnumerable<TaskDatabase>> GetTasksAsync();

	// Get all tasks that are not assigned to any user
	public Task<IEnumerable<TaskDatabase>> GetEmptyTasksAsync();

	public Task<TaskDatabase?> GetTaskAsync(Guid id);

	public Task<Boolean> CreateTaskAsync(TaskDatabase task);

	public Task<Boolean> UpdateTaskAsync(TaskDatabase task);

	public Task<Boolean> DeleteTaskAsync(Guid id);
}