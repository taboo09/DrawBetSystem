using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetSystem.Models;
using BetSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BetSystem.Repository 
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly BetDbContext context; 
        public CurrencyRepository(BetDbContext context) 
        {
            this.context = context; 
        }

        public async Task<IEnumerable<Currency>> GetCurrencies() 
        {
            var currencies = await context.Currencies.ToListAsync();

            return currencies.OrderByDescending(x => x.IsSelected);
        }

        
        public async Task<Currency> GetCurrency()
        {
            var currencies = await context.Currencies.ToListAsync();

            return currencies.Find(x => x.IsSelected);
        }

        public async Task<bool> SelectCurrency(int id)
        {
            var currencies = await context.Currencies.ToListAsync();

            var currency = currencies.Find(x => x.IsSelected);
            currency.IsSelected = false;

            currency = currencies.Find(x => x.Id == id);
            currency.IsSelected = true;

            await context.SaveChangesAsync();

            return true;
        }
    }
}