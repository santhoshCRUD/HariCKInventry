using HariCKInventry.Data;
using HariCKInventry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HariCKInventry.Pages.SubCategories
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        public CreateModel(AppDbContext db) => _db = db;

        [BindProperty]
        public SubCategory SubCategory { get; set; } = new();

        public SelectList CategoriesSelect { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var cats = await _db.Categories.AsNoTracking().ToListAsync();
            CategoriesSelect = new SelectList(cats, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }
            _db.SubCategories.Add(SubCategory);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}