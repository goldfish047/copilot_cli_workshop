using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chinook.Web.Data;
using Chinook.Web.Models;

namespace Chinook.Web.Pages.Genres
{
    public class IndexModel : PageModel
    {
        private readonly ChinookContext _context;
        public IndexModel(ChinookContext context) { _context = context; }
        public IList<Genre> Genre { get; set; }
        public async Task OnGetAsync() { Genre = await _context.Genres.AsNoTracking().ToListAsync(); }
    }
}
