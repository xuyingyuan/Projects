using System;
using System.Collections.Generic;

namespace RousinaShop.API.Data.Entities
{
    public partial class Color
    {
        public Color()
        {
            ProductColors = new HashSet<ProductColor>();
            Skus = new HashSet<Sku>();
        }

        public int Id { get; set; }
        public string ColorCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<ProductColor> ProductColors { get; set; }
        public virtual ICollection<Sku> Skus { get; set; }
    }
}
