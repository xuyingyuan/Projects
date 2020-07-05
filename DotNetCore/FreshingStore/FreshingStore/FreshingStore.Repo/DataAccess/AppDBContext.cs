using FreshingStore.Core.Entities;
using FreshingStore.Core.EntityMaps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;

namespace FreshingStore.Repo.DataAccess
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryItem> CategoryItems { get; set; }
        public virtual DbSet<Color> Colors { get; set; }

        public virtual DbSet<ImageType> ImageTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductColor> ProductColors { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<SizeCode> SizeCodes { get; set; }
        public virtual DbSet<SizeScale> SizeScales { get; set; }
        public virtual DbSet<Sku> Skus { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //new ProductMap(modelBuilder.Entity<Product>());
            //new CategoryMap(modelBuilder.Entity<Category>());
            //new SubCategoryMap(modelBuilder.Entity<SubCategory>());
            //new CategoryItemMap(modelBuilder.Entity<CategoryItem>());
            new BuildEntitiesByMapping(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder
                    .Entity(entityType.ClrType)
                    .Property(nameof(BaseEntity.Created))
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                modelBuilder
                    .Entity(entityType.ClrType)
                    .Property(nameof(BaseEntity.Modified))
                    .HasColumnType("datetime");
                modelBuilder
                   .Entity(entityType.ClrType)
                   .Property(nameof(BaseEntity.Deleted))
                   .HasColumnType("datetime");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=FreshDB");
            }

           
        }

    }
}
