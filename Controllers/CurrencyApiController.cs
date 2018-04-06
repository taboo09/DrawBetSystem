using System.Linq;
using BetSystem.Models;
using BetSystem.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace BetSystem.Controllers
{
    [Route("/api/currencies")]
    public class CurrencyApiController : Controller
    {
        private readonly BetDbContext context;
        public CurrencyApiController(BetDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult AddCurrency([FromBody] Currency currency)
        {
            if (context.Currencies.Any(x => x.Name == currency.Name)) 
                return BadRequest($"Currency {currency.Name} already exist in database!");
                
            context.Currencies.Add(currency);
            context.SaveChanges();

            return Ok(currency);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCurrency(int id)
        {
            var currency = context.Currencies.Find(id);

            if (currency == null) return NotFound($"Currency with id {id} does not exist!");

            context.Remove(currency);
            context.SaveChanges();

            return Ok($"{currency.Name} has been deleted!");
        }
    }
}