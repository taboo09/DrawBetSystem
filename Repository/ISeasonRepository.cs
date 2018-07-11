using System.Collections.Generic;
using System.Threading.Tasks;
using BetSystem.Controllers.Resources;
using BetSystem.Models;

namespace BetSystem.Repository
{
    public interface ISeasonRepository
    {
        Task<IEnumerable<Season>> GetSeasons();
        Task<bool> AddSeason(SeasonResource season);
        Task<bool> CloseSeason(int id);
        Task<bool> SelectSeason(int id);
        Task<Season> IsSelected();
        Task<bool> UpdateSeason();
    }
}