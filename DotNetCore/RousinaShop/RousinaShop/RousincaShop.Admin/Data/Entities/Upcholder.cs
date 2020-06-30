using System;
using System.Collections.Generic;

namespace RousincaShop.Admin.Data.Entities
{
    public partial class Upcholder
    {
        public int Id { get; set; }
        public string Upc { get; set; }
        public DateTime? TakenDate { get; set; }
        public DateTime? Created { get; set; }
    }
}
