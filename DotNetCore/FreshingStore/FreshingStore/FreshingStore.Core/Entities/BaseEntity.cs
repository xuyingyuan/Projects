using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FreshingStore.Core.Entities
{
    
    public class BaseEntity
    {
        public int Id { get; set; }


        public DateTime Created { get; set; }


        public DateTime? Modified { get; set; }


        public DateTime? Deleted { get; set; }
    }
}
