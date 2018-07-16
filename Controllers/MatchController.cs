using System.Threading.Tasks;
using BetSystem.Controllers.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using BetSystem.Repository;

namespace BetSystem.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchRepository matchRepository;
        private readonly ITeamRepository teamRepository;
        public MatchController(IMatchRepository matchRepository, ITeamRepository teamRepository)
        {
            this.teamRepository = teamRepository;
            this.matchRepository = matchRepository;
        }

        public async Task<IActionResult> New()
        {
            var matchResource = new MatchResource();
            matchResource.Date = DateTime.Now;
            matchResource.Teams = await teamRepository.GetTeams();

            return View("Match", matchResource);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMatch(MatchResource matchResource)
        {
            if (!ModelState.IsValid)
            {
                var matchRes = new MatchResource();
                matchRes.Date = DateTime.Now;
                matchRes.Teams = await teamRepository.GetTeams();

                return View("Match", matchRes);
                
            }
            await matchRepository.AddMatch(matchResource);

            return RedirectToAction("Index", "Home");
        }
    }
}