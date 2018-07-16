using System.Threading.Tasks;
using AutoMapper;
using BetSystem.Controllers.Resources;
using BetSystem.Models;
using BetSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BetSystem.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository teamRepository;
        private readonly ISeasonRepository seasonRepository;
        private readonly IMapper mapper;

        public TeamController(ITeamRepository teamRepository, ISeasonRepository seasonRepository, IMapper mapper)
        {
            this.teamRepository = teamRepository;
            this.seasonRepository = seasonRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> New()
        {
            ViewBag.Teams = await teamRepository.GetTeams();

            var teamResource = new TeamResource();
            
            Season selectedSeason = await seasonRepository.IsSelected();
            
            if (selectedSeason != null) {
                teamResource.SeasonId = selectedSeason.Id;
                teamResource.Season = mapper.Map<SeasonResource>(selectedSeason);
            }

            return View("AddTeam", teamResource);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeam(TeamResource teamResource)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Teams = await teamRepository.GetTeams();

                // var teamRes = new TeamResource();
                Season selectedSeason = await seasonRepository.IsSelected();
                teamResource.Season = mapper.Map<SeasonResource>(selectedSeason);   

                return View("AddTeam", teamResource);
            }
            var ok = await teamRepository.AddTeam(teamResource);

            if(!ok)
            {
                return View("Error", $"Team {teamResource.Name} already exist in database!");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}