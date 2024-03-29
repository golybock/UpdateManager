using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTableSite.Data;

namespace TimeTableSite.Pages.Roles
{
	public class EditModel : PageModel
	{
		private readonly TimeTableSite.Data.UmFullContext _context;

		public EditModel(TimeTableSite.Data.UmFullContext context)
		{
			_context = context;
		}

		[BindProperty] public Role Role { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var role = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);
			if (role == null)
			{
				return NotFound();
			}

			Role = role;
			ViewData["DepartmentId"] = new SelectList(_context.Departments.ToList(), "Id", "Name");
			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				ViewData["DepartmentId"] = new SelectList(_context.Departments.ToList(), "Id", "Name");
				return Page();
			}

			_context.Attach(Role).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!RoleExists(Role.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Index");
		}

		private bool RoleExists(int id)
		{
			return _context.Roles.Any(e => e.Id == id);
		}
	}
}