using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Data.Entities
{
    public partial class ProductImage
    {
        public int ProductImageId { get; set; }
        public int? ProductId { get; set; }
        public int? FitId { get; set; }
        public int? ColorId { get; set; }
        public string ImageType { get; set; }
        public string ProductImageUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ImageType ImageTypeNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
