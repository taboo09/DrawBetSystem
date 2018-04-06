using System; 
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc; 
using BetSystem.Persistence; 
using AutoMapper; 
using BetSystem.Repository; 

namespace BetSystem.Controllers 
{
    public class HomeController:Controller 
    {
        private readonly IStatusRepository statusRepository; 
        private readonly IBetRepository betRepository; 
        private readonly ICurrencyRepository currencyRepository; 
        public HomeController(IStatusRepository statusRepository, IBetRepository betRepository, 
                            ICurrencyRepository currencyRepository) 
        {
            this.currencyRepository = currencyRepository; 
            this.betRepository = betRepository; 
            this.statusRepository = statusRepository; 
        }

        [HttpGet]
        public async Task <IActionResult> Index() 
        {
            var teamStatusList = await statusRepository.TeamStatusList(); 

            ViewBag.Bets = await betRepository.GetBets(); 

            DateTime beginDate = new DateTime(2017, 11, 28); 
            DateTime newDate = DateTime.Now; 
            int weeks = Convert.ToInt32((newDate - beginDate).TotalDays / 7); 
            ViewBag.Weeks = weeks; 

            ViewBag.Currencies = await currencyRepository.GetCurrencies();

            ViewBag.Currency = await currencyRepository.GetCurrency();

            return View("Index", teamStatusList); 
        }

        public async Task <IActionResult> Stats() 
        {
            var teamStatusList = await statusRepository.TeamStatusList(); 

            ViewBag.ProfitList = await statusRepository.ProfitStatusList(); 

            ViewBag.Currency = await currencyRepository.GetCurrency();

            return View(teamStatusList); 
        }

        public async Task <IActionResult> SelectCurrency(int id) 
        {
            await currencyRepository.SelectCurrency(id);

            return RedirectToAction("Index");
        }
    }
}
