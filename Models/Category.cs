using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HariCKInventry.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Description { get; set; }

        public List<SubCategory> SubCategories { get; set; } = new();
        public List<Product> Products { get; set; } = new();
    }
}