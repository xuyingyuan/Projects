using FreshingStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshingStore.Core.EntityMaps
{
    public class ProductImageMap
    {
        public ProductImageMap(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImage", Constants.appSchema);
            builder.HasKey(e => e.Id);

           

            builder.Property(e => e.ImageTypeCode)                          
                            .HasMaxLength(5)
                            .IsUnicode(false);

            builder.Property(e => e.ProductImageUrl)
                            .IsRequired(true)
                           .HasMaxLength(100)
                           .IsUnicode(false);

            builder.HasOne(d => d.ImageTypeNavigation)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ImageTypeCode)
                .HasConstraintName("FK_ProductImage_ImageType");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductImage_Product");


                
        }
    }

}
