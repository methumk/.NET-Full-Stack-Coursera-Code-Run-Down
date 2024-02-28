using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/* 
    Example: Testing Authentication
        - AllowAnonymous: Makes all endpoint accessible by all users even if they aren't authenticated
*/
namespace IdentityApp.Controllers
{
    
    // [AllowAnonymous]
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Details()
        {
            // Overrides Allow anonymous attribute
            // This makes them have to be logged in (authenticated), doesn't mention privileges yet though (authorization)
            return View();
        }
        
        [Authorize]
        public IActionResult Policy()
        {
            // Overrides Allow anonymous attribute
            return View();
        }
    }
}
