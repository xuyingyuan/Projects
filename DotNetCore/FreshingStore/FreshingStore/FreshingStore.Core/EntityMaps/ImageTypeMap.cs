using FreshingStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshingStore.Core.EntityMaps
{
    public class ImageTypeMap
    {
        public ImageTypeMap(EntityTypeBuilder<ImageType> builder)
        {
            builder.ToTable("ImageType", Constants.appSchema);

            builder.HasKey(e => e.TypeCode);

            builder.Property(e => e.TypeCode)
                            .IsRequired()
                            .HasMaxLength(5)
                            .IsUnicode(false);

            builder.Property(t => t.TypeName)
               .IsRequired()
               .HasMaxLength(20)
               .IsUnicode(false);

            builder.Property(t => t.TypeDescription)              
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(t => t.FileExtension)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.IsDefaultImageType)
                .HasColumnName("isDefaultImageType");



          
        }
    }

}
