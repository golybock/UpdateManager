using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTableSite.Data;

namespace TimeTableSite.Pages.Supervisor
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
            return Page();
        }

        [BindProperty]
        public Data.Supervisor Supervisor { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Supervisors.Add(Supervisor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
