using System.Threading.Tasks;
using AutoMapper;
using BetSystem.Controllers.Resources;
using BetSystem.Persistence;
using Microsoft.AspNetCore.Mvc;
using BetSystem.Repository;

namespace BetSystem.Controllers
{
    public class BetController : Controller
    {
        private readonly IBetRepository betRepository;
        private readonly ICurrencyRepository currencyRepository;
        public BetController(IBetRepository betRepository, ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
            this.betRepository = betRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBets()
        {
            var betsResources = await betRepository.GetBets();

            ViewBag.Currency = await currencyRepository.GetCurrency();

            return View("Bets", betsResources);
        }

        public async Task<IActionResult> BetEdit(int id)
        {
            var findBet = await betRepository.FindBet(id);

            if (!findBet) return View("Error", $"Bet with id: {id} does not exist in database!");

            var betResource = await betRepository.GetBet(id);

            ViewBag.Currency = await currencyRepository.GetCurrency();

            return PartialView("_EditBet", betResource);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveBet(BetResource betResource)
        {
            if (!ModelState.IsValid)
            {
                var betsResources = await betRepository.GetBets();

                return View("Bets", betsResources);
            }

            await betRepository.SaveBet(betResource);

            return RedirectToAction("GetBets", "Bet");
        }


        public async Task<IActionResult> FindBet(int id)
        {
            var findBet = await betRepository.FindBet(id);

            if (!findBet) return View("Error", $"Bet with id: {id} does not exist in database!");

            var betResource = await betRepository.GetBet(id);

            return PartialView("_DeleteBet", betResource);
        }


        public async Task<IActionResult> DeleteBet(int id)
        {
            await betRepository.DeleteBet(id);

            return RedirectToAction("GetBets");
        }
    }
}