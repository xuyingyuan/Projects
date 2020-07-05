using System;
using System.Collections.Generic;

namespace FreshingStore.Core.Entities
{
    public partial class SizeScale:BaseEntity
    {
        public SizeScale()
        {
            Products = new HashSet<Product>();
        }
              
        public string SizeRange { get; set; }
        public string SizeCodes { get; set; }     

        public virtual ICollection<Product> Products { get; set; }
    }
}
