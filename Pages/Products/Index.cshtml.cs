using HariCKInventry.Data;
using HariCKInventry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HariCKInventry.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IndexModel(AppDbContext db) => _db = db;

        public List<Product> Products { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? q { get; set; }

        public async Task OnGetAsync()
        {
            var query = _db.Products
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .Include(p => p.PrintingParameter)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(p =>
                    p.Name.Contains(q) ||
                    p.ProductId.Contains(q));
            }

            Products = await query
                .AsNoTracking()
                .OrderByDescending(p => p.CreatedAtUtc)
                .ToListAsync();
        }
    }
}