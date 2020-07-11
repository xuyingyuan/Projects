using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreshingStore.Core.Entities
{
    public partial class ProductColor: BaseEntity
    {
       [Required]
        public int ProductId { get; set; }
        [Required]
        public int ColorId { get; set; }
        public string ColorDescription { get; set; }
        public decimal? ColorPriceOverride { get; set; }
        public bool? IsDefaultColor { get; set; }
        

        public virtual Color Color { get; set; }
        public virtual Product Product { get; set; }
    }
}
