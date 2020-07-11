using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FreshingStore.API.Models
{
    public class ProductColorDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public string ColorDescription { get; set; }
        public decimal? ColorPriceOverride { get; set; }
        public bool? IsDefaultColor { get; set; }   
        public string ColorImageUrl { get; set; }

        public virtual ICollection<ProductImageDto> ProductImageDtos { get; set; }
    }
}
