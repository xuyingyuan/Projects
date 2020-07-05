using System;
using System.Collections.Generic;

namespace FreshingStore.Core.Entities
{
    public class Color:BaseEntity
    {
        public Color()
        {
            ProductColors = new HashSet<ProductColor>();
            Skus = new HashSet<Sku>();
        }
              
        public string ColorCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       

        public virtual ICollection<ProductColor> ProductColors { get; set; }
        public virtual ICollection<Sku> Skus { get; set; }
    }
}
