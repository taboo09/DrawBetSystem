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
    [Route("/api/bets")]
    public class BetApiController : Controller
    {
        private readonly IMapper mapper;
        private readonly BetDbContext context;
        public BetApiController(BetDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<BetResource>> GetBets()
        {
            var bets = await context.Bets.Include(m => m.Match).ThenInclude(t => t.Team).ToListAsync();

            return mapper.Map<List<Bet>, List<BetResource>>(bets);
        }


        [HttpPost]
        public async Task<IActionResult> AddBet([FromBody] BetResource betResource)
        {
            var bet = mapper.Map<BetResource, Bet>(betResource);

            context.Bets.Add(bet);

            await context.SaveChangesAsync();

            return Ok(betResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBet(int id)
        {
            var bets = await context.Bets.Include(m => m.Match).ThenInclude(t => t.Team).ToListAsync();

            var bet = bets.Find(b => b.Id == id);

            if (bet == null) return NotFound("The bet you want to delete does not exist!");

            context.Bets.Remove(bet);

            await context.SaveChangesAsync();

            return Ok($"The bet id: {bet.Id} of team {bet.Match.Team.Name} has been successfully deleted!");
        }

        [HttpPost]
        public async Task<IActionResult> EditBet([FromBody] BetResource betResource)
        {
            if(betResource.Withdrawal > 0) betResource.Profit = betResource.Withdrawal;

            var bet = mapper.Map<BetResource, Bet>(betResource);

            var listBets = await context.Bets.ToListAsync();

            var betEdit = listBets.Find(x => x.Id == bet.Id);

            betEdit = bet;

            await context.SaveChangesAsync();

            return Ok();
        }

    }
}