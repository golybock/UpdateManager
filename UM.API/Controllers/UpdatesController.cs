using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UM.API.Data;
using Version = UM.API.Data.Version;

namespace UM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UpdatesController : ControllerBase
{
	private readonly UmFullContext _context;

	public UpdatesController(UmFullContext context)
	{
		_context = context;
	}

	[HttpGet("[action]")]
	public async Task<List<Version>> GetVersions()
	{
		return await _context.Versions.ToListAsync();
	}

	[HttpGet("[action]")]
	public async Task<IActionResult> DownloadUpdate(Guid id)
	{
		try
		{
			var ver = await _context.Versions.FirstOrDefaultAsync(v => v.Id == id);

			if (ver == null)
				return NotFound();

			var path = $"Updates/{ver.Path}";

			if (!System.IO.File.Exists(path))
				return NotFound();

			var bytes = await System.IO.File.ReadAllBytesAsync(path);

			return File(bytes, "application/zip");
		}
		catch (Exception e)
		{
			Console.WriteLine(e);

			return BadRequest();
		}
	}
}