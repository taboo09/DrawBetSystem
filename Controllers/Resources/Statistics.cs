namespace BetSystem.Controllers.Resources
{
    public class Statistics
    {
        public int WeeklyProfit { get; set; } = 0;
        public int TotalMatches { get; set; } = 0;
        public int TotalWon { get; set; } = 0;
        public double WonPercentage { get; set; } = 0;
        public double TotalBet { get; set; } = 0;
        public double TotalEarn { get; set; } = 0;
        public double Profit { get; set; } = 0;
        public double MaxStake { get; set; } = 0;
        public double MaxReturn { get; set; } = 0;
        public double MaxOdd { get; set; } = 0;
    }
}