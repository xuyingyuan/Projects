using FreshingStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshingStore.Core.EntityMaps
{
    public class SizeCodeMap
    {
        public SizeCodeMap(EntityTypeBuilder<SizeCode> builder)
        {
            builder.ToTable("SizeCode", Constants.appSchema);
            builder.HasKey(e => e.Code);

            builder.Property(e => e.Code)
                            .IsRequired()
                            .HasMaxLength(5)
                            .IsUnicode(false);

            builder.Property(e => e.Name)
                            .IsRequired()
                            .HasMaxLength(50)
                            .IsUnicode(false);

         
        }
    }

}
