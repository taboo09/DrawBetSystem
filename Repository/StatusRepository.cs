using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BetSystem.Models;
using BetSystem.Persistence;
using BetSystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace BetSystem.Controllers.Resources
{
    public class StatusRepository : IStatusRepository
    {
        private readonly BetDbContext context;
        private readonly ISeasonRepository seasonRepository;
        private readonly IBetRepository betRepository;

        public StatusRepository(BetDbContext context, ISeasonRepository seasonRepository, IBetRepository betRepository)
        {
            this.context = context;
            this.seasonRepository = seasonRepository;
            this.betRepository = betRepository;
        }

        public async Task<IEnumerable<TeamStatus>> TeamStatusList()
        {
            var teamStatusList = new List<TeamStatus>();

            var listTeams = await context.Teams.Include(x => x.Season).Where(x => x.Season.Selected).ToListAsync();
            var listBets = await context.Bets.Include(m => m.Match).Where(x => x.Match.Team.Season.Selected).ToListAsync();

            foreach (var item in listTeams)
            {
                var teamStatus = new TeamStatus();

                teamStatus.Id = item.Id;
                teamStatus.Name = item.Name;
                var played = listBets.Where(x => x.Match.TeamId == item.Id).Count();
                var won = listBets.Where(x => x.Match.TeamId == item.Id).Where(x => x.Won == true).Count();
                teamStatus.MatchesPlayed = played;
                teamStatus.Wons = won;

                if (won == 0) teamStatus.Percentage = 0;
                else
                {
                    teamStatus.Percentage = Math.Truncate((double)won / (double)played * 100);
                }

                var moneyBet = listBets.Where(x => x.Match.TeamId == item.Id).Sum(c => c.Match.Stake);
                var moneyEarn = listBets.Where(x => x.Match.TeamId == item.Id).Sum(c => c.Profit);
                teamStatus.MoneyBet = moneyBet;
                teamStatus.MoneyEarn = moneyEarn;
                teamStatus.Profit = Math.Round(moneyEarn - moneyBet,2);

                var lastBet = listBets.Where(x => x.Match.TeamId == item.Id).LastOrDefault();
                if (lastBet == null) teamStatus.NextStake = 1;
                else if (lastBet.Won == true) teamStatus.NextStake = 1;
                else teamStatus.NextStake = lastBet.Match.Stake * 2;

                Season selectedSeason = await seasonRepository.IsSelected();
                teamStatus.SeasonActive = selectedSeason.Active;

                teamStatusList.Add(teamStatus);
            }

            teamStatusList.Sort((x, y) => x.Name.CompareTo(strB: y.Name));

            return teamStatusList;
        }

        public async Task<IEnumerable<ProfitStatus>> ProfitStatusList()
        {
            var listBets = await context.Bets.Include(m => m.Match).Where(x => x.Match.Team.Season.Selected).ToListAsync();

            var profitStatusList = new List<ProfitStatus>();
            var datesList = new List<DateTime?>();

            foreach (var item in listBets)
            {
                datesList.Add(item.Match.Date);
            }
            datesList = datesList.Distinct().ToList();

            foreach (var d in datesList)
            {
                var profitStatus = new ProfitStatus();
                profitStatus.Date = (DateTime)d;
                profitStatus.MatchesPlayed = listBets.Where(x => x.Match.Date == d).Count();
                profitStatus.Wons = listBets.Where(x => x.Match.Date == d).Where(x => x.Won == true).Count();
                if (profitStatus.Wons == 0) profitStatus.Percentage = 0;
                else
                {
                    profitStatus.Percentage = Math.Truncate((double)profitStatus.Wons
                    / (double)profitStatus.MatchesPlayed * 100);
                }
                profitStatus.MoneyBet = listBets.Where(x => x.Match.Date == d).Sum(c => c.Match.Stake);
                profitStatus.MoneyEarn = listBets.Where(x => x.Match.Date == d).Sum(c => c.Profit);
                profitStatus.Profit = profitStatus.MoneyEarn - profitStatus.MoneyBet;

                profitStatusList.Add(profitStatus);
            }
            profitStatusList.Sort((x, y) => x.Date.CompareTo(y.Date));

            return profitStatusList;
        }

        public async Task<Statistics> Statistics()
        {
            var listBets = await betRepository.GetBets();

            var statistics = new Statistics();

            if(listBets.Any()) 
            {
                statistics.TotalMatches = listBets.Count();
                statistics.TotalWon = listBets.Where(x => x.Won == true).Count();
                statistics.TotalBet = listBets.Sum(x => x.Match.Stake);
                statistics.TotalEarn = listBets.Sum(x => x.Profit);
                statistics.WonPercentage = Math.Truncate((double)statistics.TotalWon / (double)statistics.TotalMatches * 100);
                statistics.Profit = Math.Round(statistics.TotalEarn - statistics.TotalBet, 2);
                statistics.MaxStake = listBets.Max(x => x.Match.Stake);
                statistics.MaxOdd = listBets.Any(x => x.Won == true) ? 
                    listBets.Where(x => x.Won == true).Max(x => x.Match.Odd) : 0;
                statistics.MaxReturn = listBets.Any(x => x.Won == true) ? 
                    listBets.Where(x => x.Won == true).Max(x => x.Profit) : 0;

                // getting the weeks to display weekly profit 
                int weeks = 1;

                if(listBets.Any()) {
                    var firstBet = listBets.First();
                    var lastBet = listBets.Last();            
                    weeks = Convert.ToInt32((lastBet.Match.Date - firstBet.Match.Date).TotalDays / 7);
                    weeks = weeks > 1 ? weeks : 1;
                }

                statistics.WeeklyProfit = (int)Math.Truncate(statistics.Profit/weeks);
            }

            return statistics;
        }
    }
}