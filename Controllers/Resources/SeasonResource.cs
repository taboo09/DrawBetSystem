using System.ComponentModel.DataAnnotations;

namespace BetSystem.Controllers.Resources
{
    public class SeasonResource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }    
        public bool Active { get; set; }
        public bool Selected { get; set; }
    }
}