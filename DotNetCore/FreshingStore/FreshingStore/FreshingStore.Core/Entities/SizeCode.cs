using System;
using System.Collections.Generic;

namespace FreshingStore.Core.Entities
{
    public partial class SizeCode
    {
        public SizeCode()
        {
            Skus = new HashSet<Sku>();
        }

        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Sku> Skus { get; set; }
    }
}
