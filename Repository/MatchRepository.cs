using System;
using System.Threading.Tasks;
using AutoMapper;
using BetSystem.Controllers.Resources;
using BetSystem.Models;
using BetSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BetSystem.Repository
{
    public class MatchRepository : IMatchRepository
    {
        private readonly BetDbContext context;
        private readonly IMapper mapper;
        public MatchRepository(BetDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task AddMatch(MatchResource matchResource)
        {
            matchResource.CashReturn = Math.Round(matchResource.Stake * matchResource.Odd, 2);

            var match = mapper.Map<MatchResource, Match>(matchResource);

            context.Matches.Add(match);

            var bet = new Bet();
            bet.MatchId = match.Id;

            context.Bets.Add(bet);

            await context.SaveChangesAsync();
        }

        // To be call in BetRepository, DeleteBet action
        public async Task DeleteMatch(int id)
        {
            var matches = await context.Matches.ToListAsync();
            var match = matches.Find(x => x.Id == id);

            context.Matches.Remove(match);
        }
    }
}