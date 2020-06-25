using System.Threading.Tasks;
using Basic.AuthorizationRequirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers
{
    public class OperationsController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public OperationsController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult>  Open()
        {
            var cookieJar = new CookieJar(); //may from db
            var requirement = new OperationAuthorizationRequirement() { Name = CookieJarOperations.Open };
            var result = await  _authorizationService.AuthorizeAsync(User, cookieJar, requirement);
            if(result.Succeeded)
                return View();

            return RedirectToAction("Index");

        }
    }

  
}
