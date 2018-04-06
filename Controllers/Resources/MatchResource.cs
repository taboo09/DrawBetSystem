using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BetSystem.Models;

namespace BetSystem.Controllers.Resources
{
    public class MatchResource
    {
        public int Id { get; set; }
        public TeamResource Team { get; set; }
        public int TeamId { get; set; }
        
        [Required]
        public string Home { get; set; }

        [Required]
        public string Away { get; set; }
        public DateTime Date { get; set; }
        public double Odd { get; set ; }
        public double Stake { get; set; }
        public double CashReturn { get; set; }
        public IEnumerable<TeamResource> Teams { get; set; }

        public MatchResource()
        {
            Teams = new List<TeamResource>();
        }
    }
}