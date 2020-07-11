using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreshingStore.Core.Entities
{
    public partial class Product:BaseEntity
    {
        public Product()
        {
            CategoryItems = new HashSet<CategoryItem>();
            ProductColors = new HashSet<ProductColor>();
            ProductImages = new HashSet<ProductImage>();
            Skus = new HashSet<Sku>();
        }

    
       [Required]
       [MaxLength(50)]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(300)]
        public string ProductDescription { get; set; }
        public int? SizeScaleId { get; set; }
        public decimal? Price { get; set; }
      

        public virtual SizeScale SizeScale { get; set; }
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
        public virtual ICollection<ProductColor> ProductColors { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Sku> Skus { get; set; }
    }
}
