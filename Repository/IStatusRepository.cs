using System.Collections.Generic;
using System.Threading.Tasks;
using BetSystem.Controllers.Resources;

namespace BetSystem.Repository
{
    public interface IStatusRepository
    {
        Task<IEnumerable<TeamStatus>> TeamStatusList();
        Task<IEnumerable<ProfitStatus>> ProfitStatusList();
    }
}