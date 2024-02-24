using CoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // 2. Returns the view of the index page (this action: index, was specified as the action in Program.cs for default behavior)
            // The view is tied to the index.cshtml file by name
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Message()
        {
            // Passing data from Controller to view (viewData and viewBag)
            // These are separate instances so they can have same attribute name
            ViewData["Msg"] = "This is a ViewData Example";
            ViewBag.Msg = "This is a ViewBag example";
            // return View();   // Return view here if we want

            // Pass from one action to another with TempData
            TempData["TempMessage"] = "This is a TempData Example";

            // Redirect to TempMessage Action (going to Home/Message turns into Home/TempMessage)
            return RedirectToAction("TempMessage");
            
        }

        public IActionResult TempMessage()
        {
            // This action has no idea of ViewData and ViewBag created in Message but it does have TempData access
            // NOTE!!!: TempData is only visible if the user goes to Home/Message first if they just go to /Home/TempMessage it won't be visible
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
