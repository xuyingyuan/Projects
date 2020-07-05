using System;
using System.Collections.Generic;

namespace FreshingStore.Core.Entities
{
    public partial class Sku:BaseEntity
    {
       
        public string Upc { get; set; }
        public int ProductId { get; set; }
        public int? FitId { get; set; }
        public int ColorId { get; set; }
        public string SizeCode { get; set; }
        public int? SizeIndex { get; set; }
       

        public virtual Color Color { get; set; }
        public virtual Product Product { get; set; }
        public virtual SizeCode SizeCodeNavigation { get; set; }
    }
}
