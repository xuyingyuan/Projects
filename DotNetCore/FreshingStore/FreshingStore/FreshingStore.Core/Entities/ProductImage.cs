using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreshingStore.Core.Entities
{
    public class ProductImage:BaseEntity
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public string ImageTypeCode { get; set; }
        [Required]
        public string ProductImageUrl { get; set; }     
        public virtual ImageType ImageTypeNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
