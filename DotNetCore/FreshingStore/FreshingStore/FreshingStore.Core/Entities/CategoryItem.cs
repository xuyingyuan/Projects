using System;
using System.Collections.Generic;

namespace FreshingStore.Core.Entities
{
    public partial class CategoryItem
    {
        public int SubCategoryId { get; set; }
        public int ProductId { get; set; }
        public DateTime Created { get; set; }
      

        public virtual Product Product { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
