using System.Collections.Generic;
using System.Threading.Tasks;
using BetSystem.Controllers.Resources;

namespace BetSystem.Repository
{
    public interface ITeamRepository
    {
        Task<IEnumerable<TeamResource>> GetTeams();
        Task<bool> AddTeam(TeamResource teamResource);
    }
}