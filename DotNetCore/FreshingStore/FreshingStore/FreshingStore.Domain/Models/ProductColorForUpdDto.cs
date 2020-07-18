using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshingStore.API.Models
{
    public class ProductColorForUpdDto
    {
        public string ColorDescription { get; set; }
        public decimal? ColorPriceOverride { get; set; }
        public bool? IsDefaultColor { get; set; }
        public DateTime Modified { get; set; } = DateTime.UtcNow;
    }
}
