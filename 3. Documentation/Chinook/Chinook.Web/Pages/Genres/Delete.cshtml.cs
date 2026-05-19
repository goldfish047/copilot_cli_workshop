using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chinook.Web.Data;
using Chinook.Web.Models;

namespace Chinook.Web.Pages.Genres
{
    public class DeleteModel : PageModel
    {
        private readonly ChinookContext _context;
        public DeleteModel(ChinookContext context) { _context = context; }
        [BindProperty]
        public Genre Genre { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Genre = await _context.Genres.FindAsync(id);
            if (Genre == null) return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var entity = await _context.Genres.FindAsync(Genre.GenreId);
            if (entity != null) { _context.Genres.Remove(entity); await _context.SaveChangesAsync(); }
            return RedirectToPage("Index");
        }
    }
}
