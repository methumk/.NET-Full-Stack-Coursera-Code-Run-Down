using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactMVC.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Web;

namespace ReactMVC.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly WorkoutLogDbContext _context;

        private readonly IConfiguration _config;

        private readonly PasswordHasher<UserModel> _passwordHasher;

        public UserController(WorkoutLogDbContext context, IConfiguration config, ILogger<UserController> logger)
        {
            _logger = logger;
            _context = context;
            _config = config;
            _passwordHasher = new PasswordHasher<UserModel>();
        }

        // POST: /User/Register
        [AllowAnonymous] 
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDataModel userData)
        {
            UserModel user = new UserModel(userData){Password = UserModel.HashPassword(_passwordHasher, userData.Password)};
            Console.WriteLine($"Email: {userData.Email}, {userData.Password} - hashed: {user.Password}, {userData.Username}");

            try
            {
                // Verify email and username not taken
                UserModel existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userData.Email || u.Username == userData.Username);
                if (existingUser != null)
                {
                    Console.WriteLine($"Existing user: {existingUser.Email} - {existingUser.Username}");
                    return BadRequest();
                }

                // Save user to DB
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An internal server error occurred"); ;
            }

            return CreatedAtAction(nameof(Register), new { user });
        }

        // POST: /user/login
        // [AllowAnonymous] 
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginModel)
        {
            Console.WriteLine($"Email: {userLoginModel.Email} - Pass: {userLoginModel.Password}");
            UserModel user = null;
            string token = null;

            try
            {
                // Verify email not taken...
                user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLoginModel.Email);
                if (user == null)
                {
                    Console.WriteLine($"No record of user: {userLoginModel.Email}");
                    return BadRequest();
                }

                // Verify given password matches DB hashed pass
                bool authenticated = UserModel.VerifyPassword(_passwordHasher, user.Password, userLoginModel.Password) == PasswordVerificationResult.Success;
                if (authenticated == false)
                {
                    Console.WriteLine($"Invalid authentication for: {userLoginModel.Email}");
                    return Unauthorized();
                }

                // Send JWT in authorization response
                token = UserModel.GenerateToken(_config, user);
            }catch(DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An internal server error occurred"); ;
            }

            // Return token in json format
            return CreatedAtAction(nameof(Login), new { token=token, username= user.Username, userId=user.Id});
        }
    }
}
