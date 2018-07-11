using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using BetSystem.Controllers.Resources;
using BetSystem.Models;
using BetSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BetSystem.Repository
{
    public class BetRepository : IBetRepository
    {
        private readonly BetDbContext context;
        private readonly IMapper mapper;
        private readonly IMatchRepository matchRepository;
        public BetRepository(BetDbContext context, IMapper mapper, IMatchRepository matchRepository)
        {
            this.matchRepository = matchRepository;
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<BetResource>> GetBets()
        {
            var bets = await context.Bets
                .Include(m => m.Match)
                .ThenInclude(t => t.Team)
                .ThenInclude(s => s.Season)
                .ToListAsync();

            bets = bets.Where(x => x.Match.Team.Season.Selected).OrderBy(x => x.Match.Date).ToList();

            var betsResources = mapper.Map<List<Bet>, List<BetResource>>(bets);

            return betsResources;
        }

        public async Task<bool> FindBet(int id)
        {
            if (await GetBet(id) == null) return false;
            return true;
        }

        public async Task<BetResource> GetBet(int id)
        {
            var betsResources = await GetBets();
            var betResource = betsResources.ToList().Find(x => x.Id == id);

            return betResource;
        }

        public async Task SaveBet(BetResource betResource)
        {
            var bets = await context.Bets.Include(m => m.Match).ToListAsync();
            var betEdit = bets.Find(x => x.Id == betResource.Id);

            if (betResource.Won == true)
            {
                betEdit.Won = true;

                if (betResource.Withdrawal > 0)
                {
                    betEdit.Profit = betResource.Withdrawal;
                    betEdit.Withdrawal = betResource.Withdrawal;
                }
                else betEdit.Profit = Math.Round(betEdit.Match.CashReturn, 2);
            }
            else if(betResource.Won == false)
            {
                betEdit.Won = false;
                betEdit.Profit = 0;
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteBet(int id)
        {
            var bets = await context.Bets.ToListAsync();
            var bet = bets.Find(x => x.Id == id);
            
            context.Remove(bet);
            await matchRepository.DeleteMatch(bet.MatchId);

            await context.SaveChangesAsync();
        }
    }
}