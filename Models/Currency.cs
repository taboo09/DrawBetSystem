using System.ComponentModel.DataAnnotations.Schema;

namespace BetSystem.Models
{
    [Table("Currency")]
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public bool IsSelected { get; set; }
    }
}