using UM.Models.Task.View;
using UM.Models.Tasks.Blank;

namespace UM.Tasks.Services.Services.Worker;

public interface IWorkerService
{
	public Task<IEnumerable<WorkerView>> GetWorkersAsync();

	public Task<WorkerView?> GetWorkerAsync(string login, string password);

	public Task<Boolean> CreateWorkerAsync(WorkerBlank workerBlank);
}