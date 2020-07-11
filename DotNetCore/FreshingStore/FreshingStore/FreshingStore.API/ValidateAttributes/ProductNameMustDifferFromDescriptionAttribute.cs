using FreshingStore.API.Models;
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
            var product = (ProductForCreationDto)validationContext.ObjectInstance;
            if(product.ProductName == product.ProductDescription)
            {
                return new ValidationResult(ErrorMessage,
                    new[] { nameof(ProductForCreationDto) });
            }
            return ValidationResult.Success;
        }
    }
}
