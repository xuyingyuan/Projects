using FreshingStore.API.ValidateAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreshingStore.API.Models.Product
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
