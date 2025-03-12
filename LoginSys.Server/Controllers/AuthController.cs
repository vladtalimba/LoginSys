using LoginSys.Server.Data;
using Microsoft.AspNetCore.Mvc;
using LoginSys.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace LoginSys.Server.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly AppDbContext _db;
        private PasswordHasher<Users> passwordHasher;
        private ErorrHandler error;
        
        public AuthController(AppDbContext db)
        {
            // Initialize global variables.
            _db = db;
            passwordHasher = new PasswordHasher<Users>();
        }

        public Users ReturnUser(Users user)
        {
            user.UserPassword = "";
            return user;
        }

        // Sign up
        [EnableCors("Default")]
        [HttpPost]
        [Route("signup")]
        // TODO: redirect to a dashboard page after signing up, as well as return the user.
        public async Task<IActionResult> AddUser(Users user)
        {
            if (ModelState.IsValid) {
                // After validating the model, hash user password and add entry inside db.
                string hashedPass = passwordHasher.HashPassword(user, user.UserPassword);
                user.UserPassword = hashedPass;
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
            }
            else
            {
                return BadRequest(new ErorrHandler(400, "Bad request", "The form is invalid"));
            }
            return Ok(ReturnUser(user));
        }

        // Log in
        [EnableCors("Default")]
        [HttpPost]
        [Route("login")]
        // TODO: Return the found user after all validations have been done. Redirect user to dashboard.
        // Error handling here, redirection and validation on the client side???
        public async Task<IActionResult> GetUser(Users user)
        {
            Console.WriteLine(ModelState);
            if (ModelState.IsValid)
            {
                // Get user by Email.
                Users dbUser = await _db.Users.FindAsync(user.Email);

                // If the user doesn't exist.
                if(dbUser == null)
                {
                    return NotFound(new ErorrHandler(404, "Not found", "User was not found"));
                }

                // Verify password.
                if (passwordHasher.VerifyHashedPassword(dbUser, dbUser.UserPassword, user.UserPassword) != 0)
                {
                    // Cookie settings.
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTime.UtcNow.AddDays(7),
                        IsPersistent = true,
                        IssuedUtc = DateTime.UtcNow
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // Return user.
                    return Ok(ReturnUser(dbUser));
                }else
                {
                    // If model is invalid, return error.
                    return BadRequest(new ErorrHandler(400, "Bad request", "The form is invalid"));
                }

            }
            // Return error.
            return NotFound(new ErorrHandler(404, "Not found", "User was not found"));
        }

        [EnableCors("Default")]
        [HttpGet]
        [Route("logout")]
        public async void LogOut()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
