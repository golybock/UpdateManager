using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTableSite.Data;

namespace TimeTableSite.Pages.Allowance
{
    public class EditModel : PageModel
    {
        private readonly TimeTableSite.Data.UmFullContext _context;

        public EditModel(TimeTableSite.Data.UmFullContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Allowance Allowance { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowance =  await _context.Allowances.FirstOrDefaultAsync(m => m.Id == id);
            if (allowance == null)
            {
                return NotFound();
            }
            Allowance = allowance;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Allowance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllowanceExists(Allowance.Id))
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

        private bool AllowanceExists(int id)
        {
            return _context.Allowances.Any(e => e.Id == id);
        }
    }
}
