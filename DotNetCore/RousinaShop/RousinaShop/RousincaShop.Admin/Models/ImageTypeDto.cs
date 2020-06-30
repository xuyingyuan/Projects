using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Models
{
    public partial class ImageTypeDto
    {
        public ImageTypeDto()
        {
            ProductImages = new HashSet<ProductImageDto>();
        }

        public string ImageType1 { get; set; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }
        public string FileExtension { get; set; }
        public bool? IsDefaultImageType { get; set; }

        public virtual ICollection<ProductImageDto> ProductImages { get; set; }
    }
}
