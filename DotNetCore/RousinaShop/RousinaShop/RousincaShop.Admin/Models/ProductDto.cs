using System;
using System.Collections.Generic;
using System.Linq;

namespace RousincaShop.Admin.Models
{
    public partial class ProductDto
    {
       

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int? SizeScaleId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Created { get; set; }

     
    }
}
