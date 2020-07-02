using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Data.Entities
{
    public partial class SizeScale
    {
        public SizeScale()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string SizeRange { get; set; }
        public string SizeScale1 { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
