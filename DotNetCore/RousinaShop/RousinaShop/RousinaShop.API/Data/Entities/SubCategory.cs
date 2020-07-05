using System;
using System.Collections.Generic;

namespace RousinaShop.API.Data.Entities
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            CategoryItems = new HashSet<CategoryItem>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
    }
}
