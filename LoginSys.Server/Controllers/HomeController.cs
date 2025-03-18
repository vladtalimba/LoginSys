using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LoginSys.Server.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : Controller
    {

        [Route("dashboard")]
        [EnableCors("Default")]
        [HttpGet]
        [Authorize]
        // Validate login.
        public IActionResult ValidateLogin()
        {
            if (HttpContext.Request.Cookies["LOGIN_DETAILS"] != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ErrorHandler(400, "Bad Request", "Invalid credentials"));
            }
        }
    }
}
