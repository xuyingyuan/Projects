using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            CategoryItems = new HashSet<CategoryItem>();
            ProductImages = new HashSet<ProductImage>();
            Skus = new HashSet<Sku>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int? SizeScaleId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual SizeScale SizeScale { get; set; }
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Sku> Skus { get; set; }
    }
}
