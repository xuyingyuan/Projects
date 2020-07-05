using FreshingStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshingStore.Core.EntityMaps
{
    public class ColorMap
    {
        public ColorMap(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("Color", Constants.appSchema);
            builder.HasKey(t => t.Id);


            builder.Property(e => e.ColorCode)
                            .IsRequired()
                            .HasMaxLength(3)
                            .IsUnicode(false);

            builder.Property(e => e.Name)
                            .IsRequired()
                            .HasMaxLength(50)
                            .IsUnicode(false);


            builder.Property(t => t.Description)              
                .HasMaxLength(250)
                .IsUnicode(false);

          
        }
    }

}
