using Microsoft.AspNetCore.Mvc;
using UM.Tasks.Services.Services.Task;

namespace UM.Tasks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
	private ITaskService _taskService;

	public TaskController(ITaskService taskService)
	{
		_taskService = taskService;
	}


}