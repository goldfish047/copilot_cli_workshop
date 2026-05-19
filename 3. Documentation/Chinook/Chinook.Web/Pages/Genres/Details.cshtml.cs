using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chinook.Web.Data;
using Chinook.Web.Models;

namespace Chinook.Web.Pages.Genres
{
    public class DetailsModel : PageModel
    {
        private readonly ChinookContext _context;
        public DetailsModel(ChinookContext context) { _context = context; }
        public Genre Genre { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Genre = await _context.Genres.FindAsync(id);
            if (Genre == null) return NotFound();
            return Page();
        }
    }
}
