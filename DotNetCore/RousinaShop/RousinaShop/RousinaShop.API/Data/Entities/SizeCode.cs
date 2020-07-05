using System;
using System.Collections.Generic;

namespace RousinaShop.API.Data.Entities
{
    public partial class SizeCode
    {
        public SizeCode()
        {
            Skus = new HashSet<Sku>();
        }

        public string SizeCode1 { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Sku> Skus { get; set; }
    }
}
