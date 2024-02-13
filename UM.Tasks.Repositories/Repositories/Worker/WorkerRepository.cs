using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;
using UM.Models.Tasks.Database;

namespace UM.Tasks.Repositories.Repositories.Worker;

public class WorkerRepository : NpgsqlRepository, IWorkerRepository
{
	public WorkerRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	public async Task<IEnumerable<WorkerDatabase>> GetWorkers()
	{
		throw new NotImplementedException();
	}

	public async Task<WorkerDatabase> GetWorker(string login, string password)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> CreateWorker(WorkerDatabase workerDatabase)
	{
		throw new NotImplementedException();
	}
}