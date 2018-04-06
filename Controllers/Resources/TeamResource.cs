using System.ComponentModel.DataAnnotations;

namespace BetSystem.Controllers.Resources
{
    public class TeamResource
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}