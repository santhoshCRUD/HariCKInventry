using HariCKInventry.Data;
using HariCKInventry.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HariCKInventry.Pages.SubCategories
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IndexModel(AppDbContext db) => _db = db;

        public List<SubCategory> SubCategories { get; set; } = new();

        public async Task OnGetAsync()
        {
            SubCategories = await _db.SubCategories
                .Include(s => s.Category)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}