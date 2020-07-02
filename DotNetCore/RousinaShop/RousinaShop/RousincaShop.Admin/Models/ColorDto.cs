using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Models
{
    public partial class ColorDto
    {
        public ColorDto()
        {
            Skus = new HashSet<SkuDto>();
        }

        public int Id { get; set; }
        public string ColorCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<SkuDto> Skus { get; set; }
    }
}
