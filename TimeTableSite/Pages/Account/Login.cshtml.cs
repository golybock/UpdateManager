using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTableSite.Data;
using UM.Tools.Extensions;
using Role = UM.Tools.Enums.Role;

namespace TimeTableSite.Pages.Account;

public class Login : PageModel
{
	private UmFullContext _context;

	public Login(UmFullContext context)
	{
		_context = context;
	}

	public string Error { get; set; } = string.Empty;

	[BindProperty]
	public string UserLogin { get; set; } = string.Empty;

	[BindProperty]
	public string UserPassword { get; set; } = string.Empty;


	public async Task<IActionResult> OnPostAsync()
	{
		var user = await _context.Workers
			.FirstOrDefaultAsync(c => c.Login == UserLogin);

		if (user == null)
		{
			Error = "Неверный логин";
			return Page();
		}

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.Login),
			new Claim("FullName", user.FullName),
			new Claim(ClaimTypes.Role, ((Role) user.RoleId).GetDisplayName()!),
		};

		var claimsIdentity = new ClaimsIdentity(
			claims, CookieAuthenticationDefaults.AuthenticationScheme);

		var authProperties = new AuthenticationProperties
		{
			AllowRefresh = true,
		};

		await HttpContext.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			new ClaimsPrincipal(claimsIdentity),
			authProperties);

		await _context.SaveChangesAsync();

		return LocalRedirect("/Index");
	}
}