using UM.Models.Tasks.Database;

namespace UM.Tasks.Repositories.Repositories.Worker;

public interface IWorkerRepository
{
	public Task<IEnumerable<WorkerDatabase>> GetWorkersAsync();

	public Task<WorkerDatabase?> GetWorkerAsync(Guid id);

	public Task<WorkerDatabase?> GetWorkerAsync(string login, byte[] password);

	public Task<Boolean> CreateWorkerAsync(WorkerDatabase workerDatabase);
}