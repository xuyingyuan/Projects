using System;
using System.Collections.Generic;

namespace RousinaShop.API.Data.Entities
{
    public partial class ProductColor
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public string ColorDescription { get; set; }
        public decimal? ColorPriceOverride { get; set; }
        public bool? IsDefaultColor { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Color Color { get; set; }
        public virtual Product Product { get; set; }
    }
}
