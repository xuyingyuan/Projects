using FreshingStore.Models.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreshingStore.Models.Models.Product
{
    public class ProductForUpdDto:ProductForManupulationDto
    {
        [Required(ErrorMessage ="Please fill out product description")]
        [MaxLength(300, ErrorMessage = "Product Description should be less than 300")]
        public override string ProductDescription { get; set; }
        public DateTime Modified { get; set; } = DateTime.UtcNow;
    }
}
