using FreshingStore.Models.Models;
using FreshingStore.Models.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreshingStore.API.ValidateAttributes
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
