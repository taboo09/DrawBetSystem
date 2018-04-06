using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetSystem.Models
{
    [Table("Matches")]
    public class Match
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public int TeamId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Home { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Away { get; set; }
        public DateTime Date { get; set; }
        public double Odd { get; set ; }
        public double Stake { get; set; }
        public double CashReturn { get; set; }
    }
}