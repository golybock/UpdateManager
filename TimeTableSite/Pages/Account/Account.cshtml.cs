using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTableSite.Data;
using Task = System.Threading.Tasks.Task;

namespace TimeTableSite.Pages.Account;

[Authorize]
public class Account : PageModel
{
	public Worker? Worker;

	private UmFullContext _context;


	private string? Username => User.Identity?.Name;

	public Account(UmFullContext context)
	{
		_context = context;
	}

	public async Task OnGetAsync()
	{
		Worker = await _context.Workers
			.FirstOrDefaultAsync(w => w.Login == Username);
	}

	public IActionResult OnPostLogout()
	{
		return RedirectToPage("/Account/Logout");
	}
}