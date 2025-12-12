using HariCKInventry.Data;
using HariCKInventry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HariCKInventry.Pages.SubCategories
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;
        public EditModel(AppDbContext db) => _db = db;

        [BindProperty]
        public SubCategory SubCategory { get; set; } = new();

        public SelectList CategoriesSelect { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var sc = await _db.SubCategories.FindAsync(id);
            if (sc == null) return NotFound();
            SubCategory = sc;

            var cats = await _db.Categories.AsNoTracking().ToListAsync();
            CategoriesSelect = new SelectList(cats, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var cats = await _db.Categories.AsNoTracking().ToListAsync();
                CategoriesSelect = new SelectList(cats, "Id", "Name");
                return Page();
            }
            _db.Attach(SubCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}