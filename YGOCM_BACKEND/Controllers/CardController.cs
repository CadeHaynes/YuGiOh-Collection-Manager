using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using YGOCM_BACKEND.Entities;
using YGOCM_BACKEND.Services;
using YGOCM_BACKEND.DTOs;

namespace YGOCM_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        // Database reference
        readonly AppDbContext _context;
        readonly YgoProDeckService _ypdService;

        public CardController(AppDbContext context, YgoProDeckService ypdService)
        {
            _context = context;
            _ypdService = ypdService;
        }

        // HTTP Calls
        [HttpGet] // GET all cards in the database
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
            return await _context.Cards.ToListAsync();
        }

        [HttpGet("{id}")] // GET a specific card from the database by id
        public async Task<ActionResult<Card?>> GetCardByIdFromDB(int id)
        {
            return await _context.Cards.FindAsync(id);
        }

        [HttpGet("/db/{name}")] // GET a specific card from the database by name
        public async Task<ActionResult<Card?>> GetCardByNameFromDB(string name)
        {
            var card = _context.Cards.Any(c => c.Name == name);

            return await _context.Cards.FindAsync(name);
        }

        [HttpGet("/ypd/{name}")] // GET a specific card from the API by name, using the service
        public async Task<ActionResult<YgoProDeckCard?>> GetCardByNameFromAPI(string name)
        {
            return await _ypdService.GetCardAsync(name);
        }

        [HttpPost] // POST a specific card from the API to the database
        public async Task<ActionResult<YgoProDeckCard?>> PostCardFromAPI(string name)
        {
            var card = await _ypdService.GetCardAsync(name);

            if (card == null)
            {
                return NotFound();
            }

            if (await _context.Cards.AnyAsync(c => c.Name == card.Name))
            {
                return BadRequest();
            }

            var dbCard = new Card
            {
                Name = card.Name,
                YgoProId = card.Id,
                CardType = card.Type,
                MonsterType = card.Race,
                MonsterAttribute = card.Attribute,
                MonsterLevel = card.Level,
                MonsterAttack = card.Atk,
                MonsterDefense = card.Def,
                Description = card.Desc
                //Image = card.Image
            };

            _context.Cards.Add(dbCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCardByIdFromDB), new { id = dbCard.Id }, dbCard);
        }
    }
}
