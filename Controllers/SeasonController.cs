using System;
using System.Linq;
using System.Threading.Tasks;
using BetSystem.Controllers.Resources;
using BetSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BetSystem.Controllers
{
    public class SeasonController : Controller
    {
        private readonly ISeasonRepository _seasonRepo;
        private readonly ICurrencyRepository _currencyRepository;

        public SeasonController(ISeasonRepository seasonRepo, ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
            _seasonRepo = seasonRepo;
        }

        public async Task<IActionResult> Seasons()
        {
            var listSeasons = await _seasonRepo.GetSeasons();
            var season = await _seasonRepo.IsSelected();

            if (season.Active) await _seasonRepo.UpdateSeason();

            ViewBag.Currency = await _currencyRepository.GetCurrency();

            return View(listSeasons);
        }

        // Select a season
        public async Task<IActionResult> Select(int id)
        {
            await _seasonRepo.SelectSeason(id);

            return RedirectToAction("Seasons");
        }

        // Add new season
        public async Task<IActionResult> New(SeasonResource season)
        {
            var listSeasons = await _seasonRepo.GetSeasons();

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Season name is required.";

                return View("Seasons", listSeasons);
            }

            if (!await _seasonRepo.AddSeason(season))
            {
                ViewBag.Error = "There is already a season with the same name in database.";

                return View("Seasons", listSeasons);
            }

            return RedirectToAction("Seasons");
        }

        [HttpPost]
        public IActionResult Add(SeasonResource season)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("New");
            }

            return RedirectToAction("Seasons");
        }

        // Close a season
        public async Task<IActionResult> Close(int id)
        {
            var seasons = await _seasonRepo.GetSeasons();

            var season = seasons.ToList().Find(x => x.Id == id);

            if (season != null && season.Active == true)
            {
                return PartialView("_CloseSeason", season);
            }
            else
            {
                return View("Error", "Season does not exist or it is already closed.");
            }
        }

        public async Task<IActionResult> CloseSeason(int id)
        {
            var closed = await _seasonRepo.CloseSeason(id);

            if (closed) return RedirectToAction("Seasons");

            return RedirectToAction("Seasons");
        }
    }
}