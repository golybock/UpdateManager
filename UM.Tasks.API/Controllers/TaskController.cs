using Microsoft.AspNetCore.Mvc;
using UM.Models.Tasks.Blank;
using UM.Tasks.Services.Services.Task;

namespace UM.Tasks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
	private readonly ITaskService _taskService;

	public TaskController(ITaskService taskService)
	{
		_taskService = taskService;
	}

	[HttpGet("[action]")]
	public async Task<IActionResult> GetTasksAsync()
	{
		var tasks = await _taskService.GetTasksAsync();

		return Ok(tasks);
	}

	[HttpGet("[action]")]
	public async Task<IActionResult> GetEmptyTasksAsync()
	{
		var tasks = await _taskService.GetEmptyTasksAsync();

		return Ok(tasks);
	}

	[HttpGet("[action]")]
	public async Task<IActionResult> GetTaskAsync(Guid id)
	{
		var task = await _taskService.GetTaskAsync(id);

		if(task == null)
		{
			return NotFound();
		}

		return Ok(task);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> CreateTaskAsync(TaskBlank task)
	{
		var result = await _taskService.CreateTaskAsync(task);

		return result ? Ok() : BadRequest();
	}

	[HttpPut("[action]")]
	public async Task<IActionResult> UpdateTaskAsync(Guid id, TaskBlank task)
	{
		var result = await _taskService.UpdateTaskAsync(id, task);

		return result ? Ok() : BadRequest();
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeleteTaskAsync(Guid id)
	{
		var result = await _taskService.DeleteTaskAsync(id);

		return result ? Ok() : BadRequest();
	}
}