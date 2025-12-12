using System;
using System.ComponentModel.DataAnnotations;

namespace HariCKInventry.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string ProductId { get; set; } = string.Empty;

        [Required, StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }

        // Dimensions (mm)
        [Range(0, 100000)]
        public decimal Length { get; set; }

        [Range(0, 100000)]
        public decimal Width { get; set; }

        [Range(0, 100000)]
        public decimal Height { get; set; }

        // Weight (grams)
        [Range(0, 100000)]
        public decimal Weight { get; set; }

        // Estimated print time (minutes)
        [Range(0, 100000)]
        public int PrintTimeMinutes { get; set; }

        // Cost (currency unit)
        [Range(0, 1000000)]
        public decimal Cost { get; set; }

        // File paths
        [StringLength(300)]
        public string? StlPath { get; set; }

        [StringLength(300)]
        public string? Photo1Path { get; set; }

        [StringLength(300)]
        public string? Photo2Path { get; set; }

        [StringLength(300)]
        public string? Photo3Path { get; set; }

        [StringLength(300)]
        public string? Photo4Path { get; set; }

        // Navigation
        public PrintingParameter? PrintingParameter { get; set; }

        // Audit
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; set; }
    }
}