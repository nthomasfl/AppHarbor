using Business.Entities;
using Business.Entities.DAL;
using Business.Entities.DAL.Interfaces;
using App.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace App.Controllers
{

    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        private IPollRepository pollRepository;

        public HomeController() {
            pollRepository = new PollRepository(new PollContext());
        }

        public HomeController(IPollRepository pollRepository)
        {
            this.pollRepository = pollRepository;
        }
        public ActionResult Index()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult MostPopularPolls(int? page)
        {
            IEnumerable<PollQuestion> polls=pollRepository.GetMostPopularPolls();
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(polls.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult NewestPolls(int? page)
        {
            IEnumerable<PollQuestion> polls = pollRepository.GetNewestPolls();
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(polls.ToPagedList(pageNumber, pageSize));
        }
    }
}
