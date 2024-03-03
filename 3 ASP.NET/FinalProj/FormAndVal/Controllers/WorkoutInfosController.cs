using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FormAndVal.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.ComponentModel;

namespace FormAndVal.Controllers
{
    public class WorkoutInfosController : Controller
    {
        private readonly WorkoutsContext _context;

        public WorkoutInfosController(WorkoutsContext context)
        {
            _context = context;
        }

        // GET: WorkoutInfos
        [Authorize]
        public async Task<IActionResult> Index()
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workoutsContext = _context.WorkoutInfos.Include(w => w.User).Where(w => w.User.Id == userId);

            List<WorkoutInfoGroupedByDate> grouped = await workoutsContext
                .GroupBy(w => w.Date)
                .OrderByDescending(g => g.Key)
                .Select(g => new WorkoutInfoGroupedByDate
                {
                    Date = g.Key,
                    WorkoutInfoForDate = g.ToList(),
                })
                .ToListAsync();

            return View(grouped);
        }

        // GET: WorkoutInfos/Today
        [Authorize]
        public async Task<IActionResult> Today()
        {
            DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
            ViewData["TodayDate"] = dateNow.ToString("MMMM, d, yyyy");

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workoutsContext = _context.WorkoutInfos.Include(w => w.User).Where(w => w.User.Id == userId);

            List<WorkoutInfoGroupedByDate> grouped = await workoutsContext
                .Where(m => m.Date == dateNow)
                .GroupBy(w => w.Date)
                .OrderByDescending(g => g.Key)
                .Select(g => new WorkoutInfoGroupedByDate
                {
                    Date = g.Key,
                    WorkoutInfoForDate = g.ToList(),
                })
                .ToListAsync();
            
            return View(grouped);
        }

        // GET: WorkoutInfos/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workoutInfo = await _context.WorkoutInfos
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (workoutInfo == null || workoutInfo.UserId != userId)
            {
                return NotFound();
            }

            return View(workoutInfo);
        }

        // GET: WorkoutInfos/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: WorkoutInfos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Exercise,Weight,Sets,Reps,Rest,Date")] WorkoutInfoViewModel workoutInfoVM)
        {


            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AspNetUser user = await _context.AspNetUsers.FindAsync(userId);
            if (user == null)
            {
                Console.WriteLine($"Couldn't find user for ID: {userId}");
                return NotFound();
            }

            WorkoutInfo workoutInfo = new WorkoutInfo(workoutInfoVM){User = user, UserId = userId};
            if (ModelState.IsValid)
            {
                _context.Add(workoutInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", userId);
            return View(workoutInfoVM);
        }

        // GET: WorkoutInfos/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workoutInfo = await _context.WorkoutInfos.FindAsync(id);
            if (workoutInfo == null || workoutInfo.UserId != userId)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", workoutInfo.UserId);
            return View(workoutInfo);
        }

        // POST: WorkoutInfos/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Exercise,Weight,Sets,Reps,Rest,Date")] WorkoutInfoViewModel workoutInfoVM)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                try
                {
                    WorkoutInfo workoutInfo = await _context.WorkoutInfos.FindAsync(id);
                    if (workoutInfo == null || workoutInfo.UserId != userId)
                    {
                        Console.WriteLine($"Workout Info - {id} return null workout");
                        return NotFound();
                    }
                    
                    CopyWorkoutTransferableData(workoutInfo, workoutInfoVM);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

           
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", userId);
            return View(workoutInfoVM);
        }

        // GET: WorkoutInfos/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workoutInfo = await _context.WorkoutInfos
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workoutInfo == null || workoutInfo.UserId != userId)
            {
                return NotFound();
            }

            return View(workoutInfo);
        }

        // POST: WorkoutInfos/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workoutInfo = await _context.WorkoutInfos.FindAsync(id);
            if (workoutInfo == null || workoutInfo?.UserId != userId)
            {
                return NotFound();
            }

            _context.WorkoutInfos.Remove(workoutInfo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutInfoExists(int id)
        {
            return _context.WorkoutInfos.Any(e => e.Id == id);
        }

        private void CopyWorkoutTransferableData(WorkoutInfoViewModel saving, WorkoutInfoViewModel copying)
        {
            saving.Exercise = copying.Exercise;
            saving.Weight = copying.Weight;
            saving.Sets = copying.Sets;
            saving.Reps = copying.Reps;
            saving.Rest = copying.Rest;
            saving.Date = copying.Date;
        }
    }
}
