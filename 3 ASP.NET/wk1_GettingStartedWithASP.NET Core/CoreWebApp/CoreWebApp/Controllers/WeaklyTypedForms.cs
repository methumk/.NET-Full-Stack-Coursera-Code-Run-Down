using Microsoft.AspNetCore.Mvc;

/* 
    Weakly typed forms and validations
 */
namespace CoreWebApp.Controllers
{
    public class WeaklyTypedForms : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        /* 
            This method is attached to the form from the Login view
            - We establish that the method type if POST
            - We establish that it takes in two arguments
                - NOTE: args don't have to be the same order as they were created in view
                - NOTE: args aren't case-sensitive
                - NOTE: args have to be the same letters though
        */
        [HttpPost]
        public IActionResult LoginPost(string password, string username)
        {
            ViewBag.User = username;
            ViewBag.Pass = password;
            return View();
        }
    }
}
