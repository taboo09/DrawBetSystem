using System.Threading.Tasks;
using BetSystem.Controllers.Resources;
using BetSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BetSystem.ViewComponents
{
    public class IndexStats : ViewComponent
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IBetRepository _betRepository;
        public IndexStats(IStatusRepository statusRepository, IBetRepository betRepository)
        {
            _betRepository = betRepository;
            _statusRepository = statusRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string currency)
        {
            var statistics = await _statusRepository.Statistics();

            ViewBag.Currency = currency;

            return View(statistics);
        }
    }
}