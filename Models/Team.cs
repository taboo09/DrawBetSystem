using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetSystem.Models
{
    [Table("Teams")]
    public class Team
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public int SeasonId { get; set; }
        public Season Season { get; set; }
    }
}