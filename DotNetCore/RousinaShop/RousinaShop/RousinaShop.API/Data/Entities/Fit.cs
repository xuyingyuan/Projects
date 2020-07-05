using System;
using System.Collections.Generic;

namespace RousinaShop.API.Data.Entities
{
    public partial class Fit
    {
        public int FitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
