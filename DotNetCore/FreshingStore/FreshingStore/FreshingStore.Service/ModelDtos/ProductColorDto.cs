using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FreshingStore.Service.ModelDtos
{
    class ProductColorDto
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public string ColorDescription { get; set; }
        public decimal? ColorPriceOverride { get; set; }
        public bool? IsDefaultColor { get; set; }

        public virtual Color Color { get; set; }
        public virtual ProductDto Product { get; set; }
        public string ColorImageUrl { get; set; }
    }
}
