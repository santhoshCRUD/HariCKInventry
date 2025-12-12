using HariCKInventry.Data;
using HariCKInventry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HariCKInventry.Pages.SubCategories
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _db;
        public DeleteModel(AppDbContext db) => _db = db;

        public SubCategory? SubCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            SubCategory = await _db.SubCategories.FindAsync(id);
            if (SubCategory == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var sc = await _db.SubCategories.FindAsync(id);
            if (sc == null) return NotFound();
            _db.SubCategories.Remove(sc);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}