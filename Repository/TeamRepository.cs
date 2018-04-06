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
    public class TeamRepository : ITeamRepository
    {
        private readonly BetDbContext context;
        private readonly IMapper mapper;
        public TeamRepository(BetDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<TeamResource>> GetTeams()
        {
            var listTeams = await context.Teams.ToListAsync();
            var listTeamsResource = mapper.Map<List<Team>, List<TeamResource>>(listTeams);
            listTeamsResource.Sort((x, y) => x.Name.CompareTo(strB: y.Name));

            return listTeamsResource;
        }

        public async Task<bool> AddTeam(TeamResource teamResource)
        {
            var listTeams = await GetTeams();
            
            if (listTeams.Any(x => x.Name.ToLower() == teamResource.Name.ToLower()))
            {
                return false;
            }

            var team = mapper.Map<TeamResource, Team>(teamResource);

            context.Teams.Add(team);

            await context.SaveChangesAsync();

            return true;
        }
    }
}