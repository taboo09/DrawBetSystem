using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BetSystem.Controllers.Resources;
using BetSystem.Models;
using BetSystem.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetSystem.Controllers
{
    [Route("/api/teams")]
    public class TeamApiController : Controller
    {
        private readonly BetDbContext context;
        private readonly IMapper mapper;

        public TeamApiController(BetDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<TeamResource>> GetTeams()
        {
            var teams =  await context.Teams.ToListAsync();

            return mapper.Map<List<Team>, List<TeamResource>>(teams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var team = await context.Teams.SingleOrDefaultAsync(x => x.Id == id);

            if (team == null) return NotFound("Team you are looking for does not exist!");

            var teamResource = mapper.Map<Team, TeamResource>(team);

            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam([FromBody] TeamResource teamResource)
        {
            var team = mapper.Map<TeamResource, Team>(teamResource);

            if (context.Teams.Any(x => x.Name.ToLower() == team.Name.ToLower())) 
                return BadRequest($"Team {team.Name} exists in database!");

            context.Teams.Add(team);   

            await context.SaveChangesAsync();

            return Ok(teamResource);
        }

       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteTeam(int id)
       {
           var team = await context.Teams.FindAsync(id);

           if (team == null) return NotFound("Team you want to delete does not exist!");

           context.Teams.Remove(team);

           await context.SaveChangesAsync();

           return Ok($"Team {team.Name} has been delete from database!");
       }

    }
}