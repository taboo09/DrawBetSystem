using System.Collections.Generic;
using System.Linq;
using BetSystem.Models;
using BetSystem.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BetSystem.Helper
{
    public class SeedCurrency
    {
        private readonly BetDbContext _context; 
        public SeedCurrency(BetDbContext context)
        {
            _context = context;
        }

        public async void Seed() 
        {
            // Check if there is a database
            var exists = (_context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists(); 
    
            if (exists) 
            {
                if (!_context.Currencies.Any()) 
                {
                    var currencyData = System.IO.File.ReadAllText("Persistence/Currencies.json");
                    var currencies = JsonConvert.DeserializeObject<List<Currency>>(currencyData); 
            
                    foreach (var currency in currencies) 
                    {
                        _context.Add(currency); 
                    }
                }

                await _context.SaveChangesAsync();
            }
        }     
    }
}