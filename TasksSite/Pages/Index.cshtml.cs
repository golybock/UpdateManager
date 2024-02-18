using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TasksSite.Data;
using Task = TasksSite.Data.Task;

namespace TasksSite.Pages;

public class IndexModel : PageModel
{
	private readonly ILogger<IndexModel> _logger;
	private readonly UmFullContext _context;

	// auth
	[BindProperty] public string Login { get; set; } = null!;

	[BindProperty] public string Password { get; set; } = null!;

	public bool UserSigned { get; set; }
	//

	public string? Error { get; set; }

	public List<Task> Problems { get; set; }

	public IndexModel(ILogger<IndexModel> logger, UmFullContext context)
	{
		_logger = logger;
		_context = context;

		// render problems
		Problems = _context.Tasks.Include(c => c.Status)
			.Include(c => c.Priority)
			.Include(c => c.Worker)
			.ToList();
	}

	public void OnPost()
	{
		try
		{
			var user = _context.Workers.FirstOrDefault(c => c.Login == Login && c.Password == Password);

			if (user == null)
			{
				Error = "Неверный логин или пароль";
				return;
			}

			UserSigned = true;
		}
		catch (Exception ex)
		{
			Error = "Ошибка авторизации";
			Console.Write(ex);
		}
	}
}