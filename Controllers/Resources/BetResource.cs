namespace BetSystem.Controllers.Resources
{
    public class BetResource
    {
        public int Id { get; set; }
        public MatchResource Match { get; set; }
        public int MatchId { get; set; }
        public bool? Won { get; set; }
        public double Withdrawal { get; set; }
        public double Profit { get; set; }
    }
}