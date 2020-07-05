﻿using FreshingStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshingStore.Core.EntityMaps
{
    public class SubCategoryMap
    {
        public SubCategoryMap(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable("SubCategory", Constants.appSchema);
            builder.HasKey(t => t.Id);
            builder.Property(e => e.Name)
                            .IsRequired()
                            .HasMaxLength(50)
                            .IsUnicode(false);


            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.HasOne(d => d.Category).WithMany(p => p.SubCategories).HasConstraintName("FK_SubCategory_Category");
        }
    }

}
