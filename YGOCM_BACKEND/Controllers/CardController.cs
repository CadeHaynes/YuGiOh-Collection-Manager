using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using YGOCM_BACKEND.Entities;
using YGOCM_BACKEND.DTOs;

namespace YGOCM_BACKEND.Controllers
{
    // This controller is for managing the cards in the database.
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

        [HttpGet("id/{id}")] // GET a specific card from the database by id
        public async Task<ActionResult<Card?>> GetCardById(int id)
        {
            return await _context.Cards.FindAsync(id);
        }

        [HttpGet("name/{name}")] // GET a specific card from the database by name
        public async Task<ActionResult<Card?>> GetCardByName(string name)
        {
            var card = _context.Cards.Any(c => c.Name == name);

            return await _context.Cards.FindAsync(name);
        } 

        [HttpDelete] // DELETE all cards from the database
        public async Task<IActionResult> ClearDb()
        {
            _context.Cards.RemoveRange(_context.Cards);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
