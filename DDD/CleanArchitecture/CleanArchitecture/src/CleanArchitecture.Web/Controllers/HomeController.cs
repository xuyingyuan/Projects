using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel.Interfaces;
using CleanArchitecture.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Web.Controllers
{
    public class HomeController : Controller
    {
        private  IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            
            if (!_repository.List<Guestbook>().Any())
            {
                var newGuestbook = new Guestbook() { Name = "My Guestbook" };
                newGuestbook.Entries.Add(new GuestbookEntry()
                {
                    EmailAddress = "xyz@zmy.com",
                    Message = "hi"
                ,
                    DatetimeCreated = DateTime.UtcNow.AddHours(-1)
                });
                newGuestbook.Entries.Add(new GuestbookEntry()
                {
                    EmailAddress = "efg@zmy.com",
                    Message = "hi2"
              ,
                    DatetimeCreated = DateTime.UtcNow.AddHours(-2)
                });
                newGuestbook.Entries.Add(new GuestbookEntry()
                {
                    EmailAddress = "msn@zmy.com",
                    Message = "hi3"
              ,
                    DatetimeCreated = DateTime.UtcNow.AddHours(-3)
                });

                 _repository.Add(newGuestbook);
            }

            var guestbook = _repository.GetById<Guestbook>(1);
            var viwmodel = new HomePageViewModel
                { GuestBookName = guestbook.Name, 
                PreviousEntries = guestbook.Entries };
                
            return View(viwmodel);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
