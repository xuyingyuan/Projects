using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshingStore.API.Models
{
    public class ProductColorForCreationDto
    {      
        public int ColorId { get; set; }
        public string ColorDescription { get; set; }
        public decimal? ColorPriceOverride { get; set; }
        public bool? IsDefaultColor { get; set; }       
       
    }
}
