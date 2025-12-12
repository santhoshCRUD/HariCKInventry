using HariCKInventry.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HariCKInventry.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<SubCategory> SubCategories => Set<SubCategory>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<PrintingParameter> PrintingParameters => Set<PrintingParameter>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique ProductId
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductId)
                .IsUnique();

            // Relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.SubCategories.SelectMany(sc => sc.Id == sc.Id ? new List<Product>() : new List<Product>()))
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.SubCategory)
                .WithMany()
                .HasForeignKey(p => p.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PrintingParameter>()
                .HasOne(pp => pp.Product)
                .WithOne(p => p.PrintingParameter)
                .HasForeignKey<PrintingParameter>(pp => pp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}