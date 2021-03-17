using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using projecten2.Data;
using projecten2.Models.Domain;

namespace projecten2.Areas.Identity.Pages.Streaming
{

    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
      

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
          
        }

        public IList<AppFile> DatabaseFiles { get; private set; }
      

        public async Task OnGetAsync()
        {
            DatabaseFiles = await _context.File.Include(x => x.ticket).AsNoTracking().ToListAsync();
          
        }

        public async Task<IActionResult> OnGetDownloadDbAsync(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            var requestFile = await _context.File.Include(x => x.ticket).SingleOrDefaultAsync(m => m.Id == id);

            if (requestFile == null)
            {
                return Page();
            }

            // Don't display the untrusted file name in the UI. HTML-encode the value.
            return File(requestFile.Content, MediaTypeNames.Application.Octet, WebUtility.HtmlEncode(requestFile.UntrustedName));
        }

     
    }
}
