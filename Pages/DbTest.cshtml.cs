using Microsoft.AspNetCore.Mvc.RazorPages;
using HariCKInventry.Data;

namespace HariCKInventry.Pages
{
    public class DbTestModel : PageModel
    {
        private readonly AppDbContext _context;

        public DbTestModel(AppDbContext context)
        {
            _context = context;
        }

        public string Message { get; set; } = "";

        public void OnGet()
        {
            if (_context.Database.CanConnect())
            {
                Message = "? Connected to the database successfully!";
            }
            else
            {
                Message = "? Failed to connect to the database.";
            }
        }
    }
}