using System.Threading.Tasks;
using BetSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BetSystem.ViewComponents
{
    public class SelectCurrency : ViewComponent
    {
        private readonly ICurrencyRepository _currencyRepository;

        public SelectCurrency(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currencies = await _currencyRepository.GetCurrencies();

            return View(currencies);
        }
    }
}