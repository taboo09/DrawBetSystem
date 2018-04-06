using System.Collections.Generic;
using System.Threading.Tasks;
using BetSystem.Models;

namespace BetSystem.Repository
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetCurrencies();
        Task<Currency> GetCurrency();
        Task<bool> SelectCurrency(int id);
    }
}