using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreWebApp.Data;
using CoreWebApp.Models;

/* 
    Example of depdendency injection
        - This file was scaffolded with VS based on the Dependency model class' fields
        - Step 1: Program.cs - creating db context
        - Step 2: CorWebApppContext.cs - using dbcontext as dependency of our app's context
        - step 3: DependenciesController.cs - using our app's context as a dependency for the controller
 */
namespace CoreWebApp.Controllers
{
    public class DependenciesController : Controller
    {
        private readonly CoreWebAppContext _context;

        // 3.? The "initialized" context from CoreWebAppContext.cs is injected into our Dependencies controller class as a dependency
        // NOTE: Dependency injection in the CTOR where the app context is being injected
        // If we didn't do this we would have to create a new context with the context keyword every time
        // The context depends on the DbContext
        public DependenciesController(CoreWebAppContext context)
        {
            _context = context;
        }

        // GET: Dependencies
        public async Task<IActionResult> Index()
        {
            // NOTE: using LINQ to access the Dependency data from the DB which is stored in our apps context
            return View(await _context.Dependency.ToListAsync());
        }

        // GET: Dependencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependency = await _context.Dependency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dependency == null)
            {
                return NotFound();
            }

            return View(dependency);
        }

        // GET: Dependencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dependencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email")] Dependency dependency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dependency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dependency);
        }

        // GET: Dependencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependency = await _context.Dependency.FindAsync(id);
            if (dependency == null)
            {
                return NotFound();
            }
            return View(dependency);
        }

        // POST: Dependencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email")] Dependency dependency)
        {
            if (id != dependency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dependency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependencyExists(dependency.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dependency);
        }

        // GET: Dependencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependency = await _context.Dependency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dependency == null)
            {
                return NotFound();
            }

            return View(dependency);
        }

        // POST: Dependencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dependency = await _context.Dependency.FindAsync(id);
            if (dependency != null)
            {
                _context.Dependency.Remove(dependency);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DependencyExists(int id)
        {
            return _context.Dependency.Any(e => e.Id == id);
        }
    }
}
