using App.Filters;
using App.Models;
using Business.Entities;
using Business.Entities.DAL;
using Business.Entities.DAL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using WebMatrix.WebData;

namespace App.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class PollController : Controller
    {
         private IPollRepository pollRepository;
         private IVoteRepository voteRepository;
         private UserRepository userRepository;
        public PollController() {
            pollRepository = new PollRepository(new PollContext());
            voteRepository = new VoteRepository(pollRepository.Context());
            userRepository = new UserRepository(new UserContext());
        }

        public PollController(IPollRepository pollRepository)
        {
            this.pollRepository = pollRepository;
        }

        //
        // GET: /Poll/
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            IList<PollQuestion> polls = pollRepository.GetPollsFromUser(WebSecurity.GetUserId(User.Identity.Name)).ToList();
            switch (sortOrder)
            {
                case "name_desc":
                    polls = polls.OrderByDescending(s => s.Question).ToList();
                    break;
                case "Date":
                    polls = polls.OrderBy(s => s.CreateDate).ToList();
                    break;
                case "date_desc":
                    polls = polls.OrderByDescending(s => s.CreateDate).ToList();
                    break;
                default:
                    polls = polls.OrderBy(s => s.Question).ToList();
                    break;
            }

            return View(polls);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IndexAdmin(string sortOrder, int? page)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.VoteSort = sortOrder == "vote" ? "vote_desc" : "vote";
            IList<PollQuestion> polls = pollRepository.GetPolls().ToList();

            switch (sortOrder)
            {
                case "name_desc":
                    polls = polls.OrderByDescending(s => s.Question).ToList();
                    break;
                case "vote":
                    polls = polls.OrderBy(s => s.PollVotes.Count).ToList();
                    break;
                case "vote_desc":
                    polls = polls.OrderByDescending(s => s.PollVotes.Count).ToList();
                    break;
                default:
                    polls = polls.OrderBy(s => s.Question).ToList();
                    break;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(polls.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult usersAdmin(string sortOrder, int? page)
        {
            ViewBag.IdSort = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "id";
            ViewBag.NameSort = sortOrder == "name" ? "name_desc" : "name";
            IList<UserProfile> users = userRepository.GetUsers();

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.UserName).ToList();
                    break;
                case "name":
                    users = users.OrderBy(s => s.UserName).ToList();
                    break;
                case "id_desc":
                    users = users.OrderByDescending(s => s.UserId).ToList();
                    break;
                default:
                    users = users.OrderBy(s => s.UserId).ToList();
                    break;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }
        

        public ActionResult Create(PollModel model)
        {
            if (ModelState.IsValid) {
                string[] lines = model.Answers.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() < 2) {
                    ModelState.AddModelError("AntwoordValidatie", "Vul minstens 2 antwoorden in.");
                    return View(model);
                }

                List<PollAnswer> pollAnswers = new List<PollAnswer>();
                foreach (string answer in lines)
                {
                    pollAnswers.Add(new PollAnswer() {Answer = answer});
                }

                model.Active = true;
                PollQuestion p = new PollQuestion() { 
                    Active =true,
                    Public = model.Public,
                    Question = model.Question,
                    GUID = Guid.NewGuid(),
                    CreateDate = DateTime.Now,
                    UserID = WebSecurity.CurrentUserId,
                    Answers = pollAnswers
                };
                pollRepository.InsertPoll(p);
                pollRepository.Save();
                return RedirectToAction("Index");
            }
            ModelState.Clear();
            model.Public = true;
            return View(model);
        }

        [AllowAnonymous] //Poll is beschikbaar voor niet geregistreerde bezoekers
        public ActionResult showPoll(string guid, PollVoteModel model)
        {
            PollQuestion p = pollRepository.GetPollByGUID(guid);

            if (p == null || !p.Active)
                return View(HttpNotFound());

            if (pollRepository.userVoted(WebSecurity.CurrentUserId, p.ID))
                return RedirectToAction("showResults", new { guid=guid });

            if (ModelState.IsValid)
            {
                int selectedAnswer = model.selectedID;
                PollVote pv = new PollVote() { Poll = p, Answer = pollRepository.GetAnswerbyID(selectedAnswer), UserId = WebSecurity.CurrentUserId};
                voteRepository.InsertVote(pv);
                voteRepository.Save();
                if (!WebSecurity.IsAuthenticated)
                {
                    Session.Timeout = 1440;
                    Session["votedPoll" + p.ID] = p;
                }
                return RedirectToAction("showResults", new { guid = guid });
            }

            model.poll = p;
            //voteRepository.userVoted(p.ID, WebSecurity.GetUserId(User.Identity.Name)) || Session["votedPoll" + p.ID] != null;

            return View(model);
        }

        [AllowAnonymous] //Poll is beschikbaar voor niet geregistreerde bezoekers
        public ActionResult showResults(string guid)
        {
            PollQuestion p = pollRepository.GetPollByGUID(guid);

            if (p == null || !p.Active)
                return View(HttpNotFound());

            PollVoteModel model = new PollVoteModel() { 
            poll = p,
            selectedID = voteRepository.getVotedAnswerID(WebSecurity.CurrentUserId,p.ID),
            };

            return View(model);
        }

         [Authorize(Roles = "Admin")]
        public ActionResult showPollAdmin(string guid, PollVoteAdminModel model)
        {
            PollQuestion p = pollRepository.GetPollByGUID(guid);

            if (p == null)
                return View(HttpNotFound());

            model.poll = p;
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult setActiveInactive(string guid)
        {
            PollQuestion p = pollRepository.GetPollByGUID(guid);
            p.Active = !p.Active;
            pollRepository.UpdatePoll(p);
            pollRepository.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult setPublicPrivate(string guid)
        {
            PollQuestion p = pollRepository.GetPollByGUID(guid);
            p.Public = !p.Public;
            pollRepository.UpdatePoll(p);
            pollRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
