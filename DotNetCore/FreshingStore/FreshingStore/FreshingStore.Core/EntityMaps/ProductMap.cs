using FreshingStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreshingStore.Core.EntityMaps
{
    public class ProductMap
    {
        public ProductMap(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", Constants.appSchema);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Price).HasColumnType("decimal(10,2)");
            builder.Property(t => t.ProductDescription)
                   .HasMaxLength(300)
                   .IsUnicode(true);

            builder.HasOne(d => d.SizeScale)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.SizeScaleId)
                .HasConstraintName("FK_Product_SizeScale");           

            
        }
    }

}
