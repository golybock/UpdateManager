using Microsoft.AspNetCore.Mvc;
using UM.Models.Tasks.Blank;
using UM.Tasks.Services.Services.Worker;

namespace UM.Tasks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerController : ControllerBase
{
	private readonly IWorkerService _workerService;

	public WorkerController(IWorkerService workerService)
	{
		_workerService = workerService;
	}

	public async Task<IActionResult> GetWorkersAsync()
	{
		var workers = await _workerService.GetWorkersAsync();
		return Ok(workers);
	}

	public async Task<IActionResult> GetWorkerAsync(string login, string password)
	{
		var worker = await _workerService.GetWorkerAsync(login, password);

		if (worker == null)
		{
			return NotFound();
		}

		return Ok(worker);
	}

	public async Task<IActionResult> CreateWorkerAsync(WorkerBlank workerBlank)
	{
		var result = await _workerService.CreateWorkerAsync(workerBlank);

		return result ? Ok() : BadRequest();
	}
}