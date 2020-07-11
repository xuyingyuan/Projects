using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreshingStore.API.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        
        public string ProductName { get; set; }
       
        public string ProductDescription { get; set; }
        public int? SizeScaleId { get; set; }
        public decimal? Price { get; set; }
        public DateTime Created { get; set; }
      
    }
}
