using System.Threading.Tasks;
using BetSystem.Controllers.Resources;

namespace BetSystem.Repository
{
    public interface IMatchRepository
    {
        Task AddMatch(MatchResource matchResource);
        Task DeleteMatch(int id);
    }
}