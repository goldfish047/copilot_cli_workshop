using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chinook.Web.Data;
using Chinook.Web.Models;

namespace Chinook.Web.Pages.Genres
{
    public class EditModel : PageModel
    {
        private readonly ChinookContext _context;
        public EditModel(ChinookContext context) { _context = context; }
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
            if (!ModelState.IsValid) return Page();
            _context.Attach(Genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
