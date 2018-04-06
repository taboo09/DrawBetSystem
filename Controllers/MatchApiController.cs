using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BetSystem.Controllers.Resources;
using BetSystem.Models;
using BetSystem.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetSystem.Controllers
{
    [Route("/api/matches")]
    public class MatchApiController : Controller
    {
        private readonly BetDbContext context;
        private readonly IMapper mapper;
        public MatchApiController(BetDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddMatch([FromBody] MatchResource matchResource)
        {
            var match = mapper.Map<MatchResource, Match>(matchResource);

            context.Matches.Add(match);

            await context.SaveChangesAsync();

            return Ok(matchResource);
        }

        [HttpGet]
        public async Task<IEnumerable<MatchResource>> GetMatches()
        {
            var matches = await context.Matches.Include(t => t.Team).ToListAsync();

            return mapper.Map<List<Match>, List<MatchResource>>(matches);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var matches = await context.Matches.Include(t => t.Team).ToListAsync();

            var match = matches.Find(m => m.Id == id);

            if (match == null) return NotFound("The match you want to delete does not exist!");

            context.Matches.Remove(match);

            await context.SaveChangesAsync();

            return Ok($"The match id: {match.Id} of team {match.Team.Name} has been successfully deleted!");
        }
    }
}