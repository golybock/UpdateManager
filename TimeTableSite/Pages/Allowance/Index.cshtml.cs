using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTableSite.Data;
using Task = System.Threading.Tasks.Task;

namespace TimeTableSite.Pages.Allowance
{
    public class IndexModel : PageModel
    {
        private readonly TimeTableSite.Data.UmFullContext _context;

        public IndexModel(TimeTableSite.Data.UmFullContext context)
        {
            _context = context;
        }

        public IList<Data.Allowance> Allowance { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Allowance = await _context.Allowances.ToListAsync();
        }
    }
}
