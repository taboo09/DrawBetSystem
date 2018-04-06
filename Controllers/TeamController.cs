using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BetSystem.Controllers.Resources;
using BetSystem.Models;
using BetSystem.Persistence;
using BetSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetSystem.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository teamRepository;
        public TeamController(ITeamRepository teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        public async Task<IActionResult> New()
        {
            ViewBag.Teams = await teamRepository.GetTeams();

            var teamResource = new TeamResource();

            return View("AddTeam", teamResource);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeam(TeamResource teamResource)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Teams = await teamRepository.GetTeams();

                var teamRes = new TeamResource();

                return View("AddTeam", teamRes);
            }
            var newTeam = await teamRepository.AddTeam(teamResource);

            if(!newTeam)
            {
                return View("Error", $"Team {teamResource.Name} already exist in database!");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}