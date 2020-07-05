using System;
using System.Collections.Generic;

namespace FreshingStore.Core.Entities
{
    public class SubCategory:BaseEntity
    {
        public SubCategory()
        {
            CategoryItems = new HashSet<CategoryItem>();
        }
               
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
      
        public virtual Category Category { get; set; }
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
    }
}
