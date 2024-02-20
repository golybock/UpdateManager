using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTableSite.Data;

namespace TimeTableSite.Pages.Supervisor
{
    public class DetailsModel : PageModel
    {
        private readonly TimeTableSite.Data.UmFullContext _context;

        public DetailsModel(TimeTableSite.Data.UmFullContext context)
        {
            _context = context;
        }

        public Data.Supervisor Supervisor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisors.FirstOrDefaultAsync(m => m.Id == id);
            if (supervisor == null)
            {
                return NotFound();
            }
            else
            {
                Supervisor = supervisor;
            }
            return Page();
        }
    }
}
