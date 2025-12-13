using HariCKInventry.Data;
using HariCKInventry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HariCKInventry.Pages.Products
{
    public class ViewModel : PageModel
    {
        private readonly AppDbContext _context;

        public ViewModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PartialViewResult> OnGetViewProductAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            return Partial("_ProductDetailsPartial", product);
        }
    }
}