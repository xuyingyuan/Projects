using System;
using System.Collections.Generic;

namespace FreshingStore.Core.Entities
{
    public partial class ProductColor: BaseEntity
    {
       
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public string ColorDescription { get; set; }
        public decimal? ColorPriceOverride { get; set; }
        public bool? IsDefaultColor { get; set; }
        

        public virtual Color Color { get; set; }
        public virtual Product Product { get; set; }
    }
}
