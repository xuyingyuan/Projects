using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.ResourceParameters
{
    public class AuthorResourceParameters
    {
        const int maxPageSize = 20;
        private int _pagesize = 5;
        private int _pageNumber = 1;
        public string MainCategory { get; set; }
        public string SearchQuery { get; set; }

        public int PageNumber { get; set; } = 1;

        
        public int PageSize {
            get => _pagesize;
            set => _pagesize=(value > maxPageSize) ? maxPageSize: value; 
        }

        public string OrderBy { get; set; } = "Name";

        public string Fields { get; set; }
    }
}
