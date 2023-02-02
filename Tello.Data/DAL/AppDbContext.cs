using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Data.Configurations;

namespace Tello.Data.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Variation> Variations { get; set; }
        public DbSet<VariationCategory> VariationsCategory { get; set; }
        public DbSet<VariationOption> VariationOptions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new SlideConfiguration());
            modelBuilder.ApplyConfiguration(new VariationConfiguration());
            modelBuilder.ApplyConfiguration(new VariationOptionConfiguration());
            modelBuilder.ApplyConfiguration(new VariationCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductItemConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
