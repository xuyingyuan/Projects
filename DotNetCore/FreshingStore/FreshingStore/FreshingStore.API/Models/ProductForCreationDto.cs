using FreshingStore.API.ValidateAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreshingStore.API.Models
{
    [ProductNameMustDifferFromDescription(ErrorMessage ="Product Name should be different from product description")]
    public class ProductForCreationDto //: IValidatableObject
    {
        [Required(ErrorMessage ="Product Name is required")]
        [MaxLength(50, ErrorMessage ="Product Name should be less than 50")]
        public string ProductName { get; set; }

        [Required(ErrorMessage ="Product Description is required")]
        [MaxLength(300, ErrorMessage = "Product Description should be less than 300")]
        public string ProductDescription { get; set; }
        public int? SizeScaleId { get; set; }
        public decimal? Price { get; set; }
        public DateTime Created { get; set; }

        public ICollection<ProductColorForCreationDto> ProductColors { get; set; }
                   = new List<ProductColorForCreationDto>();

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (ProductName == ProductDescription)
        //    {
        //        yield return new ValidationResult("The ProductDescription should be differnt from ProductName.",
        //            new[] { "ProductForCreationDto" });
        //    }
        //}
    }
}
