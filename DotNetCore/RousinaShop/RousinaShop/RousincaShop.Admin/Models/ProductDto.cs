using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Models
{
    public partial class ProductDto
    {
        public ProductDto()
        {
            CategoryItems = new HashSet<CategoryItemDto>();
            ProductImages = new HashSet<ProductImageDto>();
            Skus = new HashSet<SkuDto>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int? SizeScaleId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual SizeScaleDto SizeScale { get; set; }
        public virtual ICollection<CategoryItemDto> CategoryItems { get; set; }
        public virtual ICollection<ProductImageDto> ProductImages { get; set; }
        public virtual ICollection<SkuDto> Skus { get; set; }
    }
}
