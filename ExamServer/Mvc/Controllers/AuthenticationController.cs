using ExamServer.EntityFramework;
using ExamServer.EntityFramework.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamServer.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ServerDbContext _context;

        public AuthenticationController(ServerDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var dbUser = _context.Users.Where(s=>  user.Username == s.Username && s.Password == user.Password).FirstOrDefault();
            if(dbUser == null)
            {
                return Unauthorized();
            }
            return Ok(dbUser);
        }
    }
}
