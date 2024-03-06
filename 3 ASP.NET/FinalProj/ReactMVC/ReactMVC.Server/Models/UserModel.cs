using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;

namespace ReactMVC.Server.Models;

public partial class UserModel : UserDataModel
{
    [Required]
    public string Id { get; set; } = null!;

    public virtual ICollection<Workout> Workouts { get; set; } = new List<Workout>();

    public UserModel() {}
    
    public UserModel(UserDataModel udm)
    {
        Id = Guid.NewGuid().ToString();
        Email = udm.Email;
        Password = udm.Password;    //hash
        Username = udm.Username;
    }

    public static string HashPassword(PasswordHasher<UserModel> hasher, string password)
    {
        // Hash the password
        return hasher.HashPassword(null, password);
    }

    public static PasswordVerificationResult VerifyPassword(PasswordHasher<UserModel> hasher, string hashedPassword, string providedPassword)
    {
        // Verify the password
        return hasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
    }

    public static string GenerateToken(IConfiguration config, UserModel user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.System, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(24),    // NOTE: Can change token expiration time
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Get current authorized user
    // var identity = HttpContext?.User.Identity as ClaimsIdentity; <- This is the argument for the param, should be called in endpoint
    public static UserModel GetCurrentUser(ClaimsIdentity identity)
    {
        if (identity != null)
        {
            var userClaims = identity.Claims;
            return new UserModel
            {
                Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                Id = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.System)?.Value,
                Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value
            };
        }
        return null;
    }
}
