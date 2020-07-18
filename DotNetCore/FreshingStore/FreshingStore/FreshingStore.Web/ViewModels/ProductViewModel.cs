using FreshingStore.Models.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshingStore.Web.ViewModels
{
    public class ProductIndexViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();

        public ProductIndexViewModel(IEnumerable<ProductDto> productDtos)
        {
            Products = productDtos;
        }
    }
}
