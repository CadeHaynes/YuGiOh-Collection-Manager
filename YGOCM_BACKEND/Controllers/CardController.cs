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

        [HttpGet("{name}")] // GET a specific card from the API by name, using the service
        public async Task<ActionResult<YgoProDeckCard?>> GetCardByNameFromAPI(string name)
        {
            return await _ypdService.GetCardAsync(name);
        }
    }
}
