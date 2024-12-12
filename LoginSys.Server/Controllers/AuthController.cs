using LoginSys.Server.Data;
using Microsoft.AspNetCore.Mvc;
using LoginSys.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cors;

namespace LoginSys.Server.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
      private readonly AppDbContext _db;
      private PasswordHasher<Users> passwordHasher;

        
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
        public async Task<ActionResult<Users>> AddUser(Users user)
        {
            if (ModelState.IsValid) {
                // After validating the model, hash user password and add entry inside db.
                string hashedPass = passwordHasher.HashPassword(user, user.UserPassword);
                user.UserPassword = hashedPass;
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
            }
            return ReturnUser(user);
        }

        // Log in
        [EnableCors("Default")]
        [HttpPost]
        [Route("login")]
        // TODO: Return the found user after all validations have been done. Redirect user to dashboard.
        // Error handling here, redirection and validation on the client side???
        public async Task<Users> GetUser(Users user)
        {
            if (ModelState.IsValid)
            {
                // Get user by username.
                Users dbUser = await _db.Users.FindAsync(user.UserName);
                if (passwordHasher.VerifyHashedPassword(dbUser, dbUser.UserPassword, user.UserPassword) != 0)
                {
                    return ReturnUser(dbUser);
                }

            }
            return null;
        }
    }
}
