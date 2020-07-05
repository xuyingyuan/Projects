using FreshingStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshingStore.Core.EntityMaps
{
    public class CategoryItemMap
    {
        public CategoryItemMap(EntityTypeBuilder<CategoryItem> builder)
        {
            builder.ToTable("CategoryItem", Constants.appSchema);

            builder.HasKey(e => new { e.SubCategoryId, e.ProductId });

            builder.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
              

            builder.HasOne(d => d.SubCategory)
                .WithMany(p => p.CategoryItems)
                .HasForeignKey(d => d.SubCategoryId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_CategoryItem_SubCategory");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.CategoryItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_CategoryItem_Product");

               
        }
    }

}
