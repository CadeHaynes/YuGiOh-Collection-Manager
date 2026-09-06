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

    }
}
