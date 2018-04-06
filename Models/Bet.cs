using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetSystem.Models
{
    [Table("Bets")]
    public class Bet
    {
        public int Id { get; set; }
        public Match Match { get; set; }
        public int MatchId { get; set; }
        public bool? Won { get; set; }
        public double Withdrawal { get; set; }
        public double Profit { get; set; }
    }
}