using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactMVC.Server.Models;
using System.Security.Claims;

namespace ReactMVC.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly ILogger<WorkoutController> _logger;

        private readonly WorkoutLogDbContext _context;

        public WorkoutController(WorkoutLogDbContext context, ILogger<WorkoutController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // List of workouts
        // GET: /workout?page=page&recs=recs
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workout>>> GetWorkouts([FromQuery] int? page = null, [FromQuery] int? recs = null)
        {
            // Get current user - No password will be returned
            UserModel user = UserModel.GetCurrentUser(HttpContext?.User.Identity as ClaimsIdentity);
            Console.WriteLine($"User identity: {user?.Email} - {user?.Username} - {user?.Id}");
            Console.WriteLine($"Page: {page} Records: {recs}");
            if (user == null)
            {
                return Unauthorized();
            }

            if (page < 1)
            {
                return BadRequest();
            }

            List<Workout> workouts = null;
            int maxRecsPerPage = 40;
            int pageSize = (recs == null || recs > maxRecsPerPage) ? maxRecsPerPage : (int)recs;
            int startPage = page == null ? 0 : (int)page-1;     // Start page starts at index 0

            try
            {
                workouts = await _context.Workouts
                    .Where(w => w.UserId == user.Id)
                    .Skip(startPage*pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }catch(Exception)
            {
                Console.WriteLine($"Exception when getting workout at {startPage}+{pageSize} records");
                return StatusCode(500, "An internal server error occurred");
            }

            return workouts;
        }

        // Workout grouped by dates
        // GET: /workout/Dates?page=page&recs=recs
        [Authorize]
        [HttpGet]
        [Route("Dates")]
        public async Task<ActionResult<IEnumerable<WorkoutGrouped>>> GetGroupedWorkouts([FromQuery] int? page = null, [FromQuery] int? recs = null)
        {
            // Get current user - No password will be returned
            UserModel user = UserModel.GetCurrentUser(HttpContext?.User.Identity as ClaimsIdentity);
            Console.WriteLine($"User identity: {user?.Email} - {user?.Username} - {user?.Id}");
            Console.WriteLine($"Page: {page} Records: {recs}");
            if (user == null)
            {
                return Unauthorized();
            }

            if (page < 1)
            {
                return BadRequest();
            }

            List<WorkoutGrouped> workouts = null;
            int maxRecsPerPage = 40;
            int pageSize = (recs == null || recs > maxRecsPerPage) ? maxRecsPerPage : (int)recs;
            int startPage = page == null ? 0 : (int)page-1;     // Start page starts at index 0

            try
            {
                workouts = await _context.Workouts
                    .Where(w => w.UserId == user.Id)
                    .GroupBy(w => w.Date)
                    .OrderByDescending(g => g.Key)
                    .Skip(startPage*pageSize)
                    .Take(pageSize)
                    .Select(g => new WorkoutGrouped
                    {
                        Date = g.Key,
                        WorkoutInfoByDate = g.ToList(),
                    })
                    .ToListAsync();
            }catch(Exception)
            {
                Console.WriteLine($"Exception when getting grouped workout at {startPage}+{pageSize} records");
                return StatusCode(500, "An internal server error occurred");
            }

            return workouts;
        }

        // Total grouped workouts
        // GET: /workout/count
        [Authorize]
        [HttpGet]
        [Route("Count")]
        public async Task<ActionResult> GetWorkoutCount()
        {
            // Get current user - No password will be returned
            UserModel user = UserModel.GetCurrentUser(HttpContext?.User.Identity as ClaimsIdentity);
            Console.WriteLine($"User identity: {user?.Email} - {user?.Username} - {user?.Id}");
            if (user == null)
            {
                return Unauthorized();
            }

            try{
                List<WorkoutGrouped> workouts = await _context.Workouts
                    .Where(w => w.UserId == user.Id)
                    .GroupBy(w => w.Date)
                    .OrderByDescending(g => g.Key)
                    .Select(g => new WorkoutGrouped
                    {
                        Date = g.Key,
                        WorkoutInfoByDate = g.ToList(),
                    })
                    .ToListAsync();
                var data = new {
                    count = workouts.Count
                };
                return new JsonResult(data);
            }catch(Exception)
            {
                Console.WriteLine($"Exception when getting workout count");
                return StatusCode(500, "An internal server error occurred");
            }
            
        }

        // GET: /workout/id
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> GetWorkout(string id)
        {
            // Get current user - No password will be returned
            UserModel user = UserModel.GetCurrentUser(HttpContext?.User.Identity as ClaimsIdentity);
            Console.WriteLine($"User identity: {user?.Email} - {user?.Username} - {user?.Id}");
            if (user == null)
            {
                return Unauthorized();
            }

            Workout workout = null;
            try{
                // Verify workout exists and belongs to user
                workout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == id && w.UserId == user.Id);
                if (workout == null)
                {
                    return NotFound();
                }
            }catch(Exception)
            {
                Console.WriteLine($"Server error when getting workout {id}");
                return StatusCode(500, "An internal server error occurred");
            }
            
            return workout;
        }

        // PUT: /workout
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> CreateWorkout([FromBody] WorkoutDataModel workoutDataModel)
        {
            // Get current user - No password will be returned
            UserModel user = UserModel.GetCurrentUser(HttpContext?.User.Identity as ClaimsIdentity);
            Console.WriteLine($"User identity: {user?.Email} - {user?.Username} - {user?.Id}");
            if (user == null)
            {
                return Unauthorized();
            }

            Workout workout = new Workout(workoutDataModel){UserId = user.Id};
            try{
                await _context.Workouts.AddAsync(workout);
                await _context.SaveChangesAsync();
            }catch(Exception)
            {
                Console.WriteLine($"Exception when creating new workout");
                return StatusCode(500, "An internal server error occurred");
            }

            return CreatedAtAction(nameof(CreateWorkout), new { workout });
        }

        // PATCH: /workout/id
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult> EditWorkout(string id, [FromBody] WorkoutDataModel workoutDataModel)
        {
            UserModel user = UserModel.GetCurrentUser(HttpContext?.User.Identity as ClaimsIdentity);
            Console.WriteLine($"User identity: {user?.Email} - {user?.Username} - {user?.Id}");
            if (user == null)
            {
                return Unauthorized();
            }

            try
            {
                Workout workout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == id && w.UserId == user.Id);
                if (workout == null)
                {
                    return BadRequest();
                }

                workout.CopyWorkoutData(workoutDataModel);
                _context.SaveChanges();

            }catch (Exception)
            {
                Console.WriteLine($"Exception trying to edit workout: {id}");
                return StatusCode(500, "An internal server error occurred");
            }

            return Ok(new {workoutDataModel});
        }

        // DELETE: /workout/id
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorkout(string id)
        {
            UserModel user = UserModel.GetCurrentUser(HttpContext?.User.Identity as ClaimsIdentity);
            Console.WriteLine($"User identity: {user?.Email} - {user?.Username} - {user?.Id}");
            if (user == null)
            {
                return Unauthorized();
            }

            try{
                Workout workout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == id && w.UserId == user.Id);
                if (workout == null)
                {
                    return BadRequest();
                }

                _context.Workouts.Remove(workout);
                await _context.SaveChangesAsync();
            }catch(Exception)
            {
                 Console.WriteLine($"Exception trying to delete workout: {id}");
                return StatusCode(500, "An internal server error occurred");
            }

            return Ok();
        }
    }
}
