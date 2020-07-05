using FreshingStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshingStore.Core.EntityMaps
{
    public class SkuMap
    {
        public SkuMap(EntityTypeBuilder<Sku> builder)
        {
            builder.ToTable("Sku", Constants.appSchema);
            builder.HasKey(t => t.Id);

            builder.Property(e => e.Upc)
                            .IsRequired()
                            .HasMaxLength(15)
                            .IsUnicode(false);

            builder.Property(e => e.SizeCode)
                            .IsRequired()
                            .HasMaxLength(5)
                            .IsUnicode(false);

            builder.HasOne(d => d.Color)
                    .WithMany(p => p.Skus)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_Sku_Color");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.Skus)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_Sku_Product");

            builder.HasOne(d => d.SizeCodeNavigation)
                .WithMany(s => s.Skus)
                .HasForeignKey(d => d.SizeCode)
                .HasConstraintName("FK_Sku_SizeCode");



        }
    }

}
