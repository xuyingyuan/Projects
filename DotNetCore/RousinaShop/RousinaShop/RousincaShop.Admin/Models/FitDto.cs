using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Models
{
    public partial class FitDto
    {
        public int FitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
