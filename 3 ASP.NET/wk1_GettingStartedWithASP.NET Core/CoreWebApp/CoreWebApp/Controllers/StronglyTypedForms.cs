using CoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;

/* 
    Eaxmples
    - Weakly typed forms and validations
    - Model Binding
    - Form Validation in the model
 */
namespace CoreWebApp.Controllers
{
    public class StronglyTypedForms : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        /* 
            This method is attached to the form from the Login view
            - We establish that the method type if POST
            - We establish it takes the loginViewModel instead of args like in weaklyTyped Forms
        */
        [HttpPost]
        public IActionResult LoginPost(LoginViewModel lvm)
        {
            ViewBag.User = lvm.User;
            ViewBag.Pass = lvm.Pass;
            return View();
        }

        // Model binding complex object
        public IActionResult UserDetail()
        {
            var user = new LoginViewModel()
            {
                User = "Guest",
                Pass = "Pass"
            };

            // We use model binding to pass the complex object directly into the view
            // User is bound to the view
            return View(user);
        }

        // Model binding list of complex objects
        public IActionResult UserDetailList()
        {
            var users = new List<LoginViewModel>()
            {
                new LoginViewModel() {User = "Guest1", Pass="Pass1"},
                new LoginViewModel() {User = "Guest2", Pass="Pass2"},
                new LoginViewModel() {User = "Guest3", Pass="Pass3"},
            };

            return View(users);
        }

        // Form validation set up
        public IActionResult GetAccount()
        {
            return View();
        }
        
        // Form Validation
        // NOTE: we don't need to have account variable here at all for this to work correctly
        // If we want to read the value then we can use it
        [HttpPost]
        public IActionResult PostAccount(AccountValidationModel account)
        {
            // If the form input data that was bound to our model is valid then we go to success page
            // Otherwise go back to get account page
            if (ModelState.IsValid)
            {
                ViewBag.Message = $"User {account.User} was able to login";
                return View("Success");
            }
            return RedirectToAction("GetAccount");
        }
    }
}
