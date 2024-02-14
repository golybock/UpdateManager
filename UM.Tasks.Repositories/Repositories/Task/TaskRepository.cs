using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;
using UM.Models.Tasks.Database;

namespace UM.Tasks.Repositories.Repositories.Task;

public class TaskRepository : NpgsqlRepository, ITaskRepository
{
	public TaskRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	public async Task<IEnumerable<TaskDatabase>> GetTasksAsync()
	{
		var query = "SELECT * FROM task";

		return await GetListAsync<TaskDatabase>(query);
	}

	public async Task<IEnumerable<TaskDatabase>> GetEmptyTasksAsync()
	{
		var query = "SELECT * FROM task WHERE worker_id IS NULL";

		return await GetListAsync<TaskDatabase>(query);
	}

	public async Task<TaskDatabase?> GetTaskAsync(Guid id)
	{
		var query = "SELECT * FROM task WHERE id = @id";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id}
		};

		return await GetAsync<TaskDatabase>(query, parameters);
	}

	public async Task<bool> CreateTaskAsync(TaskDatabase task)
	{
		var query = "INSERT INTO task (id, description, priority, status, start_time, client_email) " +
		            "VALUES ($1, $2, $3, $4, $5, $6)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = task.Id},
			new NpgsqlParameter() {Value = task.Description},
			new NpgsqlParameter() {Value = task.Priority},
			new NpgsqlParameter() {Value = task.Status},
			new NpgsqlParameter() {Value = task.StartTime},
			new NpgsqlParameter() {Value = task.ClientEmail}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> UpdateTaskAsync(TaskDatabase task)
	{
		var query = "UPDATE task SET description = $2, priority = $3, status = $4, start_time = $5, worker_id = $6 " +
		            "WHERE id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = task.Id},
			new NpgsqlParameter() {Value = task.Description},
			new NpgsqlParameter() {Value = task.Priority},
			new NpgsqlParameter() {Value = task.Status},
			new NpgsqlParameter() {Value = task.StartTime},
			new NpgsqlParameter() {Value = task.WorkerId}
		};

		return await ExecuteAsync(query, parameters);
	}

	public Task<bool> DeleteTaskAsync(Guid id)
	{
		return DeleteAsync("task", "id", id);
	}
}