using FreshingStore.Model.ValidateAttributes;
using System.ComponentModel.DataAnnotations;

namespace FreshingStore.Models.Models.Product
{
    [ProductNameMustDifferFromDescription(ErrorMessage
       = "Product Name should be different from product description")]
    public abstract class ProductForManupulationDto
    {
        [Required(ErrorMessage = "Product Name is required")]
        [MaxLength(50, ErrorMessage = "Product Name should be less than 50")]
        public string ProductName { get; set; }

       
        [MaxLength(300, ErrorMessage = "Product Description should be less than 300")]
        public virtual  string ProductDescription { get; set; }
        public int? SizeScaleId { get; set; }
        public decimal? Price { get; set; }
    }
}
