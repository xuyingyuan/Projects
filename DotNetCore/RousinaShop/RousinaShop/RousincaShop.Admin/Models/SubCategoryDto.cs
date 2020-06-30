using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Models
{
    public partial class SubCategoryDto
    {
        public SubCategoryDto()
        {
            CategoryItems = new HashSet<CategoryItemDto>();
        }

        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual CategoryDto Category { get; set; }
        public virtual ICollection<CategoryItemDto> CategoryItems { get; set; }
    }
}
