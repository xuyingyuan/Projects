using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Data.Entities
{
    public partial class Color
    {
        public Color()
        {
            Skus = new HashSet<Sku>();
        }

        public int ColorId { get; set; }
        public string ColorCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<Sku> Skus { get; set; }
    }
}
