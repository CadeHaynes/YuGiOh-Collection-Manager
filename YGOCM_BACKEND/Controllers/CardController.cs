using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using YGOCM_BACKEND.Entities;

namespace YGOCM_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        // Database reference
        readonly AppDbContext _context;

        public CardController(AppDbContext context)
        {
            _context = context;
        }

        // HTTP Calls
        [HttpGet] // GET all cards in the database
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
            return await _context.Cards.ToListAsync();
        }
    }
}
