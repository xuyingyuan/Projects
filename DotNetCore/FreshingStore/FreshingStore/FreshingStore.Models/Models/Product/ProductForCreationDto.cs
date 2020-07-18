using FreshingStore.Models.Models.Product;
using System.Collections.Generic;

namespace FreshingStore.Models.Models.Product
{

    public class ProductForCreationDto: ProductForManupulationDto //: IValidatableObject
    {
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
