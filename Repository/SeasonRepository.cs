using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BetSystem.Controllers.Resources;
using BetSystem.Models;
using BetSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BetSystem.Repository
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly BetDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBetRepository _betRepository;
        private readonly ITeamRepository _teamRepository;

        public SeasonRepository(BetDbContext context, 
            IMapper mapper,
            IBetRepository betRepository,
            ITeamRepository teamRepository)
        {
            _mapper = mapper;
            _betRepository = betRepository;
            _teamRepository = teamRepository;
            _context = context;
        }

        public async Task<bool> AddSeason(SeasonResource seasonResource)
        {
            var exist = await _context.Seasons.AnyAsync(x => x.Name == seasonResource.Name);
            
            if(exist) return false;

            var season = _mapper.Map<Season>(seasonResource);
            season.DateStart = DateTime.Now;
            season.Active = true;

            if (season.Selected) {
                var seasonSelected = await IsSelected();
                seasonSelected.Selected = false;
            }

            _context.Seasons.Add(season);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CloseSeason(int id)
        {
            var seasons = await _context.Seasons.ToListAsync();

            var season = seasons.Find(x => x.Id == id);

            season.Active = false;
            season.DateEnd = DateTime.Now;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Season>> GetSeasons()
        {
            return await _context.Seasons.ToListAsync();
        }

        public async Task<Season> IsSelected()
        {
            var seasons = await _context.Seasons.ToListAsync();

            return seasons.Find(x => x.Selected);
        }


        public async Task<bool> SelectSeason(int id)
        {
            var seasons = await _context.Seasons.ToListAsync();

            var season = seasons.Find(x => x.Selected);
            season.Selected = false;

            season = seasons.Find(x => x.Id == id);
            season.Selected = true;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSeason()
        {
            var season = await IsSelected();

            var bets = await _betRepository.GetBets();
            var teams = await _teamRepository.GetTeams();

            season.Teams = teams.Count();
            season.Bets = bets.Count(); 
            season.Profit = Math.Round(bets.Sum(x => x.Profit) - bets.Sum(x => x.Match.Stake), 2);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}