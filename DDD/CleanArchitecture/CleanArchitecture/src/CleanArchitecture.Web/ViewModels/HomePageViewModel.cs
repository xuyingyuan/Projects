using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Web.ViewModels
{
    public class HomePageViewModel
    {
        public string GuestBookName { get; set; }
        public List<GuestbookEntry> PreviousEntries { get; set; } = new List<GuestbookEntry>();
    }
}
