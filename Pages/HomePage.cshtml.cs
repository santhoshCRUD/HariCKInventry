using Microsoft.AspNetCore.Mvc.RazorPages;
using HariCKInventry.Data;   // your DbContext namespace
using Microsoft.EntityFrameworkCore;

namespace HariCKInventry.Pages
{
    public class HomePageModel : PageModel
    {
        private readonly AppDbContext _context;

        public HomePageModel(AppDbContext context)
        {
            _context = context;
        }

        // Properties exposed to UI
        public int ProductCount { get; set; }
        public int CategoryCount { get; set; }
        public int SubCategoryCount { get; set; }

        public async Task OnGetAsync()
        {
            ProductCount = await _context.Products.CountAsync();
            CategoryCount = await _context.Categories.CountAsync();
            SubCategoryCount = await _context.SubCategories.CountAsync();
        }
    }
}
