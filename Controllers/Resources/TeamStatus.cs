namespace BetSystem.Controllers.Resources
{
    public class TeamStatus
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public int MatchesPlayed { get; set; }
        public int Wons { get; set; }
        public double Percentage { get; set; }
        public double MoneyBet { get; set; }
        public double MoneyEarn { get; set; }
        public double NextStake { get; set; }
        public double Profit { get; set; }
        public bool SeasonActive { get; set; }
    }
}