using System;
using System.Collections.Generic;

namespace RousinaShop.API.Data.Entities
{
    public partial class Sku
    {
        public int Id { get; set; }
        public string Upc { get; set; }
        public int ProductId { get; set; }
        public int? FitId { get; set; }
        public int? ColorId { get; set; }
        public string SizeCode { get; set; }
        public int? SizeIndex { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Color Color { get; set; }
        public virtual Product Product { get; set; }
        public virtual SizeCode SizeCodeNavigation { get; set; }
    }
}
