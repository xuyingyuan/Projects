using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Models
{
    public class ProductDetailDto
    {
        public ProductDetailDto()
        {
       
            ProductImagesDtos = new List<ProductImageDto>();
            SkusDtos = new List<SkuDto>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int? SizeScaleId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Created { get; set; }

        public virtual SizeScaleDto SizeScale { get; set; }
    
        public virtual IEnumerable<ProductImageDto> ProductImagesDtos { get; set; }
        public virtual IEnumerable<SkuDto> SkusDtos { get; set; }
    }
}
