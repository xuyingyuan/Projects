using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Data.Entities
{
    public partial class ImageType
    {
        public ImageType()
        {
            ProductImages = new HashSet<ProductImage>();
        }

        public string ImageType1 { get; set; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }
        public string FileExtension { get; set; }
        public bool? IsDefaultImageType { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
