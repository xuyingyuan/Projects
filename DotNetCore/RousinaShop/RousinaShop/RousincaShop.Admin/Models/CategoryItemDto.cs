﻿using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Models
{
    public partial class CategoryItemDto
    {
        public int SubCategoryId { get; set; }
        public int ProductId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ProductDto Product { get; set; }
        public virtual SubCategoryDto SubCategory { get; set; }
    }
}
