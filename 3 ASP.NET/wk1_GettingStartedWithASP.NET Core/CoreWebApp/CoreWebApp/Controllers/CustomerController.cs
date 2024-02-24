using CoreWebApp.Models;        // Import customers from the models namespace
using Microsoft.AspNetCore.Mvc;

/* 
    This specifies a new Route (new controller) at /Customer/<action>
    Example for:
        - Razor markup keywords using @ within view
        - ViewBag/Data in view
        - TempData
        - Session
            - SetString
            - GetString
            - Remove
        - @foreach keyword in view
        - Layouts in view
        - HTML helpers in the view
            - Anchor tag with @Html.ActionLink()
        - Tag helper in view
            - Anchor tag example
        - Attribute routing
 */
namespace CoreWebApp.Controllers
{
    public class CustomerController : Controller
    {
        public static List<Customer> customers = new List<Customer>()
        {
            new Customer(){ Id = 101, Name="King", Amount=12000 },
            new Customer(){ Id = 102, Name="Jester", Amount=50 },
        };

        public IActionResult Index()
        {
            // Using the view bag keyword and storing value in the "message" field
            // NOTE: You could also use ViewData here, view Data uses dictionary action types instead of dynamic property
            // Both can get/set, ViewBag is a wrapper around view data both accomplish the same purporse of transfering data from controller to view
            
            ViewBag.Message = "Customer Management System";
            // NOTE: they both go to the same thing so above viewBag is changed to below
            ViewData["Message"] = "Customer Management System - data";

            ViewBag.CustomerCount = customers.Count();
            ViewBag.CustomerList = customers;
            return View();
        }

        public IActionResult TempDataEx()
        {
            // This uses temp data
            // NOTE basically a session but in format of ViewData and it allow to move data from controller to controller or between two actions or from view <-> controller
            TempData["Message"] = "Customer Management System - data";
            TempData["CustomerCount"] = customers.Count();
            TempData["CustomerList"] = customers;

            // NOTE: for whatever reason trying to save customer list causes the TempDataEx page to fail to be displayed
            // This ensures that the TempData persists for the next request
            TempData.Keep("CustomerCount");
            return View();
        }

        public IActionResult CheckTempData()
        {
            // This uses temp data defined in TempDataEx
            // Make sure to check that the value is not null
            if (TempData["Message"] == null  || TempData["CustomerCount"] == null)
            {
                // Redirects to the Index action
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult SessionEx()
        {
            // Create the session variable at this endpoint
            HttpContext.Session.SetString("username", "guest");

            // This will directly transfer requests made to /Customer/SessionEx -> SessionCheckEx
            return RedirectToAction("SessionCheckEx");
        }

        public IActionResult SessionCheckEx()
        {
            // NOTE: !If the user goes to Customer/SessionCheckEx first the session variable was not created yet (done in SessionEx) and won't be displayed)
            // Get the session and save it temporarily in a view bag for this endpoint
            ViewBag.Username = HttpContext.Session.GetString("username");
            return View();
        }

        public IActionResult SessionRemoveEx()
        {
            // Delete the session variable form this endpoint
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }

        public IActionResult QueryEx()
        {
            string name = "DefaultName";
            // If given the format: /Customer/QueryEx?name={value}
            // Retrieve the name query string from the URL if it's not null or empty
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["name"]))
            {
                name = HttpContext.Request.Query["name"];
            }

            ViewBag.Name = name;
            return View();
        }


        public IActionResult Details()
        {
            // This view maps explicitly to a view that uses the _Layout layout
            return View();
        }

        public IActionResult NoLayout()
        {
            // This view maps to a view that doesn't use the base _Layout
            return View();
        }

        // This overrides default path for the app or conventious routing (e.g. conventious routing set to index as default)
        // [Route("~/")]   
        // THis maps the URL to http://localhost:5296/AttributeRouting NOTE: not specifying the controller at all
        [Route("/AttributeRouting")]
        public IActionResult AttributeRouting()
        {
            // Explicitly defining that the view is called Details.cshtml
            return View("Details");
        }
    }
}
