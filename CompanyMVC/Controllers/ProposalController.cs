using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyMVC.Models;
using CompanyMVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMVC.Controllers
{
    [Authorize]
    public class ProposalController : Controller
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IProposalRepository _proposalRepository;

        public ProposalController(IConferenceRepository conferenceRepository, IProposalRepository proposalRepository)
        {
            _conferenceRepository = conferenceRepository;
            _proposalRepository = proposalRepository;
        }

        public IActionResult Index(int conferenceId)
        {
            var conference = _conferenceRepository.GetById(conferenceId);
            ViewBag.Title = $"Speaker - Proposals For Conference {conference.Name} {conference.Location}";
            ViewBag.ConferenceId = conferenceId;

            return View(_proposalRepository.GetAllForConference(conferenceId));
        }

        [Authorize(Policy = "SpeakerAccessPolicy")]
        public IActionResult AddProposal(int conferenceId)
        {
            ViewBag.Title = "Speaker - Add Proposal";
            return View(new Proposal { ConferenceId = conferenceId });
        }

        [HttpPost]
        public IActionResult AddProposal(Proposal proposal)
        {
            if (ModelState.IsValid) { 
                _proposalRepository.Add(proposal);
                _proposalRepository.Commit();
            }
            return RedirectToAction("Index", new { conferenceId = proposal.ConferenceId });
        }

        [Authorize(Policy = "OrganizerAccessPolicy")]
        public async Task<IActionResult> Approve(int proposalId)
        {
            var proposal = _proposalRepository.Approve(proposalId);
            _proposalRepository.Commit();

            return RedirectToAction("Index", new { conferenceId = proposal.ConferenceId });
        }
    }
}