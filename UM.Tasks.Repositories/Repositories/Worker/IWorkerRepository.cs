using UM.Models.Tasks.Database;

namespace UM.Tasks.Repositories.Repositories.Worker;

public interface IWorkerRepository
{
	public Task<IEnumerable<WorkerDatabase>> GetWorkers();

	public Task<WorkerDatabase> GetWorker(string login, string password);

	public Task<Boolean> CreateWorker(WorkerDatabase workerDatabase);
}