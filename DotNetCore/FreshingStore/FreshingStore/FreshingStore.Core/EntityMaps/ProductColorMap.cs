using FreshingStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshingStore.Core.EntityMaps
{
    public class ProductColorMap
    {
        public ProductColorMap(EntityTypeBuilder<ProductColor> builder)
        {
            builder.ToTable("ProductColor", Constants.appSchema);

            builder.HasKey(t => t.Id);
            builder.Property(e => e.ColorDescription)
                            .HasMaxLength(100)
                            .IsUnicode(false);

            builder.Property(e => e.ColorPriceOverride)
                .HasColumnType("decimal(10,2)");

            builder.Property(e => e.IsDefaultColor)
               .HasDefaultValueSql("((0))");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.ProductColors)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_ProductColor_Product");

            builder.HasOne(d => d.Color)
                .WithMany(p => p.ProductColors)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey("FK_ProductColor_Color");
        }
    }

}
