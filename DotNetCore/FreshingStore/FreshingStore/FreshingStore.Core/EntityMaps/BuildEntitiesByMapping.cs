using FreshingStore.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreshingStore.Core.EntityMaps
{
    public class BuildEntitiesByMapping
    {
        public BuildEntitiesByMapping(ModelBuilder modelBuilder)
        {
            new ProductMap(modelBuilder.Entity<Product>());
            new CategoryMap(modelBuilder.Entity<Category>());
            new SubCategoryMap(modelBuilder.Entity<SubCategory>());
            new CategoryItemMap(modelBuilder.Entity<CategoryItem>());
            new ColorMap(modelBuilder.Entity<Color>());
            new SkuMap(modelBuilder.Entity<Sku>());
            new SizeScaleMap(modelBuilder.Entity<SizeScale>());
            new ProductImageMap(modelBuilder.Entity<ProductImage>());
            new ProductColorMap(modelBuilder.Entity<ProductColor>());
            new ImageTypeMap(modelBuilder.Entity<ImageType>());
            new SizeCodeMap(modelBuilder.Entity<SizeCode>());
          
        }
        
    }

}
