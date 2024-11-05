using LoginSys.Server.Data;
using Microsoft.AspNetCore.Mvc;
using LoginSys.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace LoginSys.Server.Controllers
{
    [ApiController]
    [Route("signup")]
    public class SignUpController : Controller
    {
      private readonly AppDbContext _db;

        public SignUpController(AppDbContext db)
        {
            _db = db;
        }

        [EnableCors("Default")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _db.Users.ToListAsync();
        }

        [EnableCors("Default")]
        [HttpPost]
        public async Task<ActionResult<Users>> AddUser(Users user)
        {
            if (ModelState.IsValid) {
                PasswordHasher<Users> passwordHasher = new PasswordHasher<Users>();
                string hashedPass = passwordHasher.HashPassword(user, user.UserPassword);
                user.UserPassword = hashedPass;
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(GetUsers), user);
        }
    }
}
