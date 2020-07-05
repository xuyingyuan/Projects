using FreshingStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshingStore.Core.EntityMaps
{
    public class CategoryMap
    {
        public CategoryMap(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category", Constants.appSchema);
            builder.HasKey(t => t.Id);
            builder.Property(e => e.Name)
                            .IsRequired()
                            .HasMaxLength(50)
                            .IsUnicode(false);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);           
        }
    }

}
