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

// <div class="select-currency show-toggle" style="display:none">
//             @using (Html.BeginForm("SelectCurrency", "Home"))
//             {
//                 <div class="form-group">
//                     @Html.DropDownList("Id", new SelectList(currencies, "Id", "Name"), new { @class="form-control" })
//                 </div>
//                 <button type="submit" class="btn btn-secondary">Select</button>
//             }
//         </div>
// @{ IEnumerable<BetSystem.Models.Currency> currencies = ViewBag.Currencies; }