using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Models
{
    public partial class ProductImageDto
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? FitId { get; set; }
        public int? ColorId { get; set; }
        public string ImageType { get; set; }
        public string ProductImageUrl { get; set; }
        public DateTime Created { get; set; }

        public string ImageTypeDescription { get; set; }
        //public virtual ImageTypeDto ImageTypeNavigation { get; set; }
        //public virtual ProductDto Product { get; set; }

     
    }
}
