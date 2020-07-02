using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Models
{
    public partial class SkuDto
    {
        public int Id { get; set; }
        public string Upc { get; set; }
        public int ProductId { get; set; }
        public int? FitId { get; set; }
        public int? ColorId { get; set; }
        public string SizeCode { get; set; }
        public int? SizeIndex { get; set; }
        public DateTime Created { get; set; }
  
        public string SizeName { get; set; }

        public virtual ColorDto Color { get; set; }
        public virtual ProductDto Product { get; set; }
     
    }
}
