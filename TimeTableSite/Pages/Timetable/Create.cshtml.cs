using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTableSite.Data;

namespace TimeTableSite.Pages.Timetable
{
	public class CreateModel : PageModel
	{
		private readonly TimeTableSite.Data.UmFullContext _context;

		public CreateModel(TimeTableSite.Data.UmFullContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			ViewData["PeriodId"] = new SelectList(_context.Periods.ToList(), "Id", "Name");
			ViewData["SupervisorId"] = new SelectList(_context.Supervisors.ToList(), "Id", "Name");
			return Page();
		}

		[BindProperty] public Data.Timetable Timetable { get; set; } = default!;

		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				ViewData["PeriodId"] = new SelectList(_context.Periods.ToList(), "Id", "Name");
				ViewData["SupervisorId"] = new SelectList(_context.Supervisors.ToList(), "Id", "Name");
				return Page();
			}

			_context.Timetables.Add(Timetable);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}