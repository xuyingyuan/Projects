
using FreshingStore.Models.Models;
using FreshingStore.Models.Models.Product;
using System.ComponentModel.DataAnnotations;

namespace FreshingStore.Model.ValidateAttributes
{
    public class ProductNameMustDifferFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, 
            ValidationContext validationContext)
        {
            var containerType = validationContext.ObjectInstance.GetType();

            var product = (ProductForManupulationDto)value; //validationContext.ObjectInstance;
            
                if (product.ProductName == product.ProductDescription)
                {
                    return new ValidationResult(ErrorMessage,
                        new[] { nameof(ProductColorForManipulationDto) });                   
                }
           
            return ValidationResult.Success;
        }
    }
}
