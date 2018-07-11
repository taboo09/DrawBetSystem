using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetSystem.Models
{
    [Table("Seasons")]
    public class Season
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public bool Active { get; set; }
        public bool Selected { get; set; }
        public int Teams { get; set; }
        public int Bets { get; set; }
        public double Profit { get; set; }
    }
}