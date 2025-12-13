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
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly FileStorageService _files;
        public EditModel(AppDbContext db, FileStorageService files) { _db = db; _files = files; }

        public SelectList CategoriesSelect { get; set; } = default!;
        public SelectList SubCategoriesSelect { get; set; } = default!;

        [BindProperty] public Product Product { get; set; } = new();
        [BindProperty] public PrintingParameter PrintingParameter { get; set; } = new();

        [BindProperty] public IFormFile? StlFile { get; set; }
        [BindProperty] public IFormFile? Photo1 { get; set; }
        [BindProperty] public IFormFile? Photo2 { get; set; }
        [BindProperty] public IFormFile? Photo3 { get; set; }
        [BindProperty] public IFormFile? Photo4 { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var prod = await _db.Products
                .Include(p => p.PrintingParameter)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prod == null) return NotFound();
            Product = prod;
            PrintingParameter = prod.PrintingParameter ?? new PrintingParameter { ProductId = prod.Id };

            CategoriesSelect = new SelectList(await _db.Categories.AsNoTracking().ToListAsync(), "Id", "Name");
            SubCategoriesSelect = new SelectList(await _db.SubCategories.AsNoTracking().ToListAsync(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CategoriesSelect = new SelectList(await _db.Categories.AsNoTracking().ToListAsync(), "Id", "Name");
                SubCategoriesSelect = new SelectList(await _db.SubCategories.AsNoTracking().ToListAsync(), "Id", "Name");
                return Page();
            }

            var existing = await _db.Products.Include(p => p.PrintingParameter).FirstOrDefaultAsync(p => p.Id == Product.Id);
            if (existing == null) return NotFound();

            // Update scalar fields
            existing.ProductId = Product.ProductId;
            existing.Name = Product.Name;
            existing.Description = Product.Description;
            existing.CategoryId = Product.CategoryId;
            existing.SubCategoryId = Product.SubCategoryId;
            existing.Length = Product.Length;
            existing.Width = Product.Width;
            existing.Height = Product.Height;
            existing.Weight = Product.Weight;
            existing.PrintTimeMinutes = Product.PrintTimeMinutes;
            existing.Cost = Product.Cost;
            existing.UpdatedAtUtc = System.DateTime.UtcNow;

            // Uploads
            if (StlFile != null) existing.StlPath = await _files.SaveStlAsync(StlFile.FileName, StlFile.OpenReadStream());
            async Task<string?> SavePhoto(IFormFile? f) => f == null ? null : await _files.SavePhotoAsync(f.FileName, f.OpenReadStream());
            if (Photo1 != null) existing.Photo1Path = await SavePhoto(Photo1);
            if (Photo2 != null) existing.Photo2Path = await SavePhoto(Photo2);
            if (Photo3 != null) existing.Photo3Path = await SavePhoto(Photo3);
            if (Photo4 != null) existing.Photo4Path = await SavePhoto(Photo4);

            // Printing parameters
            if (existing.PrintingParameter == null)
            {
                PrintingParameter.ProductId = existing.Id;
                _db.PrintingParameters.Add(PrintingParameter);
            }
            else
            {
                existing.PrintingParameter.Material = PrintingParameter.Material;
                existing.PrintingParameter.Color = PrintingParameter.Color;
                existing.PrintingParameter.LayerHeight = PrintingParameter.LayerHeight;
                existing.PrintingParameter.InfillPercent = PrintingParameter.InfillPercent;
                existing.PrintingParameter.NozzleTemp = PrintingParameter.NozzleTemp;
                existing.PrintingParameter.BedTemp = PrintingParameter.BedTemp;
                existing.PrintingParameter.PrinterModel = PrintingParameter.PrinterModel;
            }

            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}