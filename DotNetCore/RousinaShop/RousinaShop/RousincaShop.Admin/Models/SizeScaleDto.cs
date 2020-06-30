using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Models
{
    public partial class SizeScaleDto
    {
        public SizeScaleDto()
        {
            Products = new HashSet<ProductDto>();
        }

        public int SizeScaleId { get; set; }
        public string SizeRange { get; set; }
        public string SizeScale1 { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<ProductDto> Products { get; set; }
    }
}
