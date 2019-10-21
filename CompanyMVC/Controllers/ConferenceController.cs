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
    public class ConferenceController : Controller
    {
        private readonly IConferenceRepository _conferenceRepository;

        public ConferenceController(IConferenceRepository conferenceRepository)
        {
            _conferenceRepository = conferenceRepository;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Organizer - Conference Overview";
            return View(_conferenceRepository.GetAll);
        }

        [Authorize(Policy = "OrganizerAccessPolicy")]
        public IActionResult Add()
        {
            ViewBag.Title = "Organizer - Add Conference";
            return View(new Conference());
        }

        [HttpPost]
        [Authorize(Policy = "OrganizerAccessPolicy")]
        public IActionResult Add(Conference conference)
        {
            if (ModelState.IsValid)
            {
                _conferenceRepository.Add(conference);
                _conferenceRepository.Commit();
            } 

            return RedirectToAction("Index");
        }
    }
}