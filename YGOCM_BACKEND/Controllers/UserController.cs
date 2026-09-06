using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using YGOCM_BACKEND.Entities;

namespace YGOCM_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // HTTP Calls
        [HttpGet] // GET all users in the database
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost] // POST a new user to the database
        public async Task<ActionResult<User>> CreateNewUser(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
