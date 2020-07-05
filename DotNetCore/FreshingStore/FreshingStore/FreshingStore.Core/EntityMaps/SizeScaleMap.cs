using FreshingStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshingStore.Core.EntityMaps
{
    public class SizeScaleMap
    {
        public SizeScaleMap(EntityTypeBuilder<SizeScale> builder)
        {
            builder.ToTable("SizeScale", Constants.appSchema);
            builder.HasKey(t => t.Id);
            builder.Property(e => e.SizeRange)
                            .IsRequired()
                            .HasMaxLength(10)
                            .IsUnicode(false);

            builder.Property(t => t.SizeRange)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
   
        }
    }

}
