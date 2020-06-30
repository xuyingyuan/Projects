using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Models
{
    public partial class CategoryDto
    {
        public CategoryDto()
        {
            SubCategories = new HashSet<SubCategoryDto>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<SubCategoryDto> SubCategories { get; set; }
    }
}
