using System.Security.Cryptography;
using UM.Models.Task.View;
using UM.Models.Tasks.Blank;
using UM.Models.Tasks.Database;
using UM.Models.Tasks.Domain;
using UM.Tasks.Repositories.Repositories.Worker;
using UM.Tools.Crypto;

namespace UM.Tasks.Services.Services.Worker;

public class WorkerService(IWorkerRepository workerRepository) : IWorkerService
{
	public async Task<IEnumerable<WorkerView>> GetWorkersAsync()
	{
		var workersDatabase = await workerRepository.GetWorkersAsync();

		var workersDomain = workersDatabase
			.Select(wd => new WorkerDomain(wd))
			.ToList();

		var workersView = workersDomain
			.Select(wd => new WorkerView(wd))
			.ToList();

		return workersView;
	}

	public async Task<WorkerView?> GetWorkerAsync(string login, string password)
	{
		var passwordHash = Sha256Hash.Hash(password);

		var worker = await workerRepository.GetWorkerAsync(login, passwordHash);

		if (worker == null)
			return null;

		var workerDomain = new WorkerDomain(worker);

		var workerView = new WorkerView(workerDomain);

		return workerView;
	}

	public async Task<bool> CreateWorkerAsync(WorkerBlank workerBlank)
	{
		var workerDatabase = new WorkerDatabase(workerBlank)
		{
			Password = Sha256Hash.Hash(workerBlank.Password)
		};

		var result = await workerRepository.CreateWorkerAsync(workerDatabase);

		return result;
	}
}