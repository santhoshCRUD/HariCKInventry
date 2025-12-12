using HariCKInventry.Data;
using HariCKInventry.Models;
using HariCKInventry.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HariCKInventry.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly FileStorageService _files;

        public CreateModel(AppDbContext db, FileStorageService files)
        {
            _db = db;
            _files = files;
        }

        public SelectList CategoriesSelect { get; set; } = default!;
        public SelectList SubCategoriesSelect { get; set; } = default!;

        [BindProperty] public Product Product { get; set; } = new();
        [BindProperty] public PrintingParameter PrintingParameter { get; set; } = new();

        [BindProperty] public IFormFile? StlFile { get; set; }
        [BindProperty] public IFormFile? Photo1 { get; set; }
        [BindProperty] public IFormFile? Photo2 { get; set; }
        [BindProperty] public IFormFile? Photo3 { get; set; }
        [BindProperty] public IFormFile? Photo4 { get; set; }

        public async Task OnGetAsync()
        {
            CategoriesSelect = new SelectList(await _db.Categories.AsNoTracking().ToListAsync(), "Id", "Name");
            SubCategoriesSelect = new SelectList(await _db.SubCategories.AsNoTracking().ToListAsync(), "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            if (StlFile != null)
                Product.StlPath = await _files.SaveStlAsync(StlFile.FileName, StlFile.OpenReadStream());

            async Task<string?> SavePhoto(IFormFile? f)
                => f == null ? null : await _files.SavePhotoAsync(f.FileName, f.OpenReadStream());

            Product.Photo1Path = await SavePhoto(Photo1);
            Product.Photo2Path = await SavePhoto(Photo2);
            Product.Photo3Path = await SavePhoto(Photo3);
            Product.Photo4Path = await SavePhoto(Photo4);

            _db.Products.Add(Product);
            await _db.SaveChangesAsync();

            PrintingParameter.ProductId = Product.Id;
            _db.PrintingParameters.Add(PrintingParameter);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}