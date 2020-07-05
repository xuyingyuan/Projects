using System;
using System.Collections.Generic;

namespace FreshingStore.Core.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }
                
        public string Name { get; set; }
        public string Description { get; set; }
       
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
