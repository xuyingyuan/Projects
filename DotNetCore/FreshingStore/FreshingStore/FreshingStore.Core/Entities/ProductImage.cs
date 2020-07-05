using System;
using System.Collections.Generic;

namespace FreshingStore.Core.Entities
{
    public class ProductImage:BaseEntity
    {       
        public int ProductId { get; set; }      
        public int ColorId { get; set; }
        public string ImageTypeCode { get; set; }
        public string ProductImageUrl { get; set; }     
        public virtual ImageType ImageTypeNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
