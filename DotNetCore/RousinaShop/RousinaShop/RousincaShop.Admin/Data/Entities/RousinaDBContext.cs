using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RousincaShop.Admin.Data.Entities
{
    public partial class RousinaDBContext : DbContext
    {
        public RousinaDBContext()
        {
        }

        public RousinaDBContext(DbContextOptions<RousinaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryItem> CategoryItems { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Fit> Fits { get; set; }
        public virtual DbSet<ImageType> ImageTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<SizeCode> SizeCodes { get; set; }
        public virtual DbSet<SizeScale> SizeScales { get; set; }
        public virtual DbSet<Sku> Skus { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Upcholder> Upcholders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=RousinaDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category", "product");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategoryItem>(entity =>
            {
                entity.HasKey(e => new { e.SubCategoryId, e.ProductId });

                entity.ToTable("CategoryItem", "product");

                entity.Property(e => e.SubCategoryId).HasColumnName("subCategoryId");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryItem_Product");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.CategoryItems)
                    .HasForeignKey(d => d.SubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryItem_SubCategory");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color", "product");

                entity.Property(e => e.ColorCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Fit>(entity =>
            {
                entity.ToTable("Fit", "product");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImageType>(entity =>
            {
                entity.HasKey(e => e.ImageType1);

                entity.ToTable("ImageType", "product");

                entity.Property(e => e.ImageType1)
                    .HasColumnName("ImageType")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FileExtension)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IsDefaultImageType).HasColumnName("isDefaultImageType");

                entity.Property(e => e.TypeDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeName)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "product");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.SizeScale)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SizeScaleId)
                    .HasConstraintName("FK_Product_SizeScale");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("ProductImage", "product");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.ImageType)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.ProductImageUrl)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ImageTypeNavigation)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ImageType)
                    .HasConstraintName("FK_ProductImage_ImageType");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductImage_Product");
            });

            modelBuilder.Entity<SizeCode>(entity =>
            {
                entity.HasKey(e => e.SizeCode1);

                entity.ToTable("SizeCode", "product");

                entity.Property(e => e.SizeCode1)
                    .HasColumnName("SizeCode")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SizeScale>(entity =>
            {
                entity.ToTable("SizeScale", "product");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.SizeRange)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SizeScale1)
                    .HasColumnName("SizeScale")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sku>(entity =>
            {
                entity.ToTable("SKU", "product");

                entity.Property(e => e.Skuid).HasColumnName("SKUId");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.SizeCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Upc)
                    .IsRequired()
                    .HasColumnName("UPC")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Skus)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_SKU_Color");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Skus)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SKU_Product");

                entity.HasOne(d => d.SizeCodeNavigation)
                    .WithMany(p => p.Skus)
                    .HasForeignKey(d => d.SizeCode)
                    .HasConstraintName("FK_SKU_SizeCode");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.ToTable("SubCategory", "product");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategory_Category");
            });

            modelBuilder.Entity<Upcholder>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UPCHolder", "product");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.TakenDate).HasColumnType("datetime");

                entity.Property(e => e.Upc)
                    .HasColumnName("UPC")
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
