using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using YGOCM_BACKEND.Entities;
using YGOCM_BACKEND.Services;
using YGOCM_BACKEND.DTOs;

namespace YGOCM_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YpdController : ControllerBase
    {
        // Database reference
        readonly AppDbContext _context;
        readonly YgoProDeckService _ypdService;

        public YpdController(AppDbContext context, YgoProDeckService ypdService)
        {
            _context = context;
            _ypdService = ypdService;
        }

        // HTTP Calls
        [HttpGet("{name}")] // GET a specific card from the API by name, using the service
        public async Task<ActionResult<YgoProDeckCard?>> GetCardByNameFromAPI(string name)
        {
            return await _ypdService.GetCardAsync(name);
        }

        [HttpPost("name/{name}")] // POST a specific card from the API to the database
        public async Task<ActionResult<YgoProDeckCard>> PostCardFromAPI(string name)
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

            return card;
        }

        [HttpPost("count/{count}")] // POST a specific card from the API to the database
        public async Task<ActionResult<IEnumerable<YgoProDeckCard>>> PostXCardsFromAPI(int count)
        {
            var cards = await _ypdService.GetXCardsAsync(count);

            if (cards == null)
            {
                return NotFound();
            }

            foreach (var card in cards)
            {
                if (!await _context.Cards.AnyAsync(c => c.Name == card.Name))
                {
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
                }
            }
            
            await _context.SaveChangesAsync();

            return cards.ToList<YgoProDeckCard>();
        }
    }
}
