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

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductId)
                .IsUnique();

            // Product → Category (many-to-one)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)   // <-- simple property
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product → SubCategory (many-to-one)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.SubCategory)
                .WithMany(sc => sc.Products) // <-- simple property
                .HasForeignKey(p => p.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product → PrintingParameter (one-to-one)
            modelBuilder.Entity<PrintingParameter>()
                .HasOne(pp => pp.Product)
                .WithOne(p => p.PrintingParameter)
                .HasForeignKey<PrintingParameter>(pp => pp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}