using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chinook.Web.Data;
using Chinook.Web.Models;

namespace Chinook.Web.Pages.Genres
{
    public class CreateModel : PageModel
    {
        private readonly ChinookContext _context;
        public CreateModel(ChinookContext context) { _context = context; }
        [BindProperty]
        public Genre Genre { get; set; }
        public void OnGet() { }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            _context.Genres.Add(Genre);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
