using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Models
{
    public class ProductColorDto
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

        public virtual ColorDto Color { get; set; }

        public ProductImageDto defaultProductImageDto { get; set;}
        public IEnumerable<ProductImageDto> productImageDtos { get; set; }
   
    }
}
