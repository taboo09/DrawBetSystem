using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BetSystem.Repository;
using BetSystem.Controllers.Resources;

namespace BetSystem.Controllers
{
    public class HomeController : Controller 
    {
        private readonly IStatusRepository statusRepository; 
        private readonly ICurrencyRepository currencyRepository; 
        public HomeController(IStatusRepository statusRepository, ICurrencyRepository currencyRepository) 
        {
            this.currencyRepository = currencyRepository; 
            this.statusRepository = statusRepository; 
        }

        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            var teamStatusList = await statusRepository.TeamStatusList(); 

            ViewBag.Currencies = await currencyRepository.GetCurrencies();

            ViewBag.Currency = await currencyRepository.GetCurrency();

            return View("Index", teamStatusList); 
        }

        public async Task<IActionResult> Stats() 
        {
            var viewModelProfitStatus = new ViewModelProfitStatus();

            viewModelProfitStatus.TeamStatusList = await statusRepository.TeamStatusList(); 

            viewModelProfitStatus.ProfitStatusList = await statusRepository.ProfitStatusList(); 

            ViewBag.Currency = await currencyRepository.GetCurrency();

            return View(viewModelProfitStatus); 
        }

        public async Task<IActionResult> SelectCurrency(int id) 
        {
            await currencyRepository.SelectCurrency(id);

            return RedirectToAction("Index");
        }
    }
}
