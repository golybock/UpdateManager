using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTableSite.Data;

namespace TimeTableSite.Pages.Timetable
{
	public class EditModel : PageModel
	{
		private readonly TimeTableSite.Data.UmFullContext _context;

		public EditModel(TimeTableSite.Data.UmFullContext context)
		{
			_context = context;
		}

		[BindProperty] public Data.Timetable Timetable { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var timetable = await _context.Timetables.FirstOrDefaultAsync(m => m.Id == id);
			if (timetable == null)
			{
				return NotFound();
			}

			Timetable = timetable;
			ViewData["PeriodId"] = new SelectList(_context.Periods.ToList(), "Id", "Name");
			ViewData["SupervisorId"] = new SelectList(_context.Supervisors.ToList(), "Id", "Name");
			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				ViewData["PeriodId"] = new SelectList(_context.Periods.ToList(), "Id", "Name");
				ViewData["SupervisorId"] = new SelectList(_context.Supervisors.ToList(), "Id", "Name");
				return Page();
			}

			_context.Attach(Timetable).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TimetableExists(Timetable.Id))
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

		private bool TimetableExists(int id)
		{
			return _context.Timetables.Any(e => e.Id == id);
		}
	}
}