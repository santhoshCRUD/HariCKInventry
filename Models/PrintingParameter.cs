using System.ComponentModel.DataAnnotations;

namespace HariCKInventry.Models
{
    public class PrintingParameter
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [StringLength(50)]
        public string? Material { get; set; } // e.g., PLA, ABS

        [StringLength(50)]
        public string? Color { get; set; }

        [Range(0, 1000)]
        public decimal LayerHeight { get; set; } // mm

        [Range(0, 100)]
        public int InfillPercent { get; set; }

        [Range(0, 1000)]
        public decimal NozzleTemp { get; set; } // C

        [Range(0, 1000)]
        public decimal BedTemp { get; set; } // C

        [StringLength(100)]
        public string? PrinterModel { get; set; }
    }
}