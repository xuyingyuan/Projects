using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Models
{
    public partial class SizeCodeDto
    {
        public SizeCodeDto()
        {
            Skus = new HashSet<SkuDto>();
        }

        public string SizeCode1 { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SkuDto> Skus { get; set; }
    }
}
