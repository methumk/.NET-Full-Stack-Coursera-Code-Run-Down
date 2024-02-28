using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORMApp.Models;

namespace DomainFirst.Controllers
{

    /* Examples CRUD
        - Starting with scaffolding to create, read, update, and delete (CRUD) data
     */
    public class CustomerController : Controller
    {
        // 1. Create a readonly link to the context
        // Not modifiable and not accessible outside class
        private readonly ApplicationDBContext _context;

        // 2. Create an instance of the DB context so we can use it (note not DEP INJECT)
        // With context set up now we should be able to talk to the DB from ORM through the context
        public CustomerController()
        {
            // NOTE: not using dependency injection
            _context = new ApplicationDBContext();
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            // Gets all customers by communicating with the DB to create a list of customer objects
            var customers = _context.Customers.ToList();
            // Using model binding to set data over to the view
            return View(customers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
