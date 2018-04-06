using System.Collections.Generic;
using System.Threading.Tasks;
using BetSystem.Controllers.Resources;

namespace BetSystem.Repository
{
    public interface IBetRepository
    {
         Task<IEnumerable<BetResource>> GetBets();
         Task<bool> FindBet(int id);
         Task<BetResource> GetBet(int id);
         Task SaveBet(BetResource betResource);
         Task DeleteBet(int id);
    }
}