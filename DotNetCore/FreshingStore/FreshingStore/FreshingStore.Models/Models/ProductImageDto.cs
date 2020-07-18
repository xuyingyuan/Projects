using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshingStore.Models.Models
{
    public class ProductImageDto
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public string ImageTypeCode { get; set; }
        public string ProductImageUrl { get; set; }
      
    }
}
