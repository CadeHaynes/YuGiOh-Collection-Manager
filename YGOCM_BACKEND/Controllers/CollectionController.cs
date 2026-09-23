using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using YGOCM_BACKEND.Entities;
using System.Security.Claims;

namespace YGOCM_BACKEND.Controllers
{
    // This controller is for managing collection entries
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController : ControllerBase
    {
        // Database reference
        readonly AppDbContext _context;

        public CollectionController(AppDbContext context)
        {
            _context = context;
        }

        /*
        // HTTP Calls
        [HttpGet("user/{id}")] // GET a specific user's collection entry from the database by their id
        public async Task<ActionResult<IEnumerable<CollectionEntry>>> GetCollectionByUserId(int id)
        {
            return await _context.CollectionEntries
                .Where(c => c.UserId == id)
                .Include(c => c.User)
                .Include(c => c.Card)
                .ToListAsync();
        }
        */

        [Authorize]
        [HttpGet] // GET a specific user's collection entry from the database by their id
        public async Task<ActionResult<IEnumerable<CollectionEntry>>> GetAuthorizedCollection()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var collection = await _context.CollectionEntries
                .Where(c => c.UserId == userId)
                .Include(c => c.Card)
                .ToListAsync();

            return collection;
        }
        
        [Authorize]
        [HttpPost] // POST a new collection entry to the database using auth
        public async Task<ActionResult<CollectionEntry>> PostCollectionEntryAuth(int cardId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var entry = new CollectionEntry
            {
                UserId = userId,
                CardId = cardId,
                Quantity = quantity
            };

            _context.CollectionEntries.Add(entry);
            await _context.SaveChangesAsync();

            return Ok(entry);
        }
        
        /*[HttpPost] // POST a new collection entry to the database
        public async Task<ActionResult<CollectionEntry>> PostCollectionEntry(int userId, int cardId, int quantity)
        {
            if (await _context.Users.FirstOrDefaultAsync(u => u.Id == userId) == null)
            {
                return NotFound("User not found");
            }

            if (await _context.Cards.FirstOrDefaultAsync(c => c.Id == cardId) == null)
            {
                return NotFound("Card not found");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.Id == cardId);

            var entry = new CollectionEntry
            {
                //User = user,
                UserId = userId,
                Card = card,
                CardId = cardId,
                Quantity = quantity
            };
            
            _context.CollectionEntries.Add(entry);
            await _context.SaveChangesAsync();

            return entry;
        }*/
    }
}
