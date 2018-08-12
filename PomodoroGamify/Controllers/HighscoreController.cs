using PomodoroGamify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace PomodoroGamify.Controllers
{
    public class HighscoreController : Controller
    {

        private ApplicationDbContext _context;

        public HighscoreController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Highscore
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();

            var user = _context.UserModels.SingleOrDefault(c => c.UserId == userID);

            return View(user);
        }

        public ActionResult PomodoroCompleted()
        {

            string userID = User.Identity.GetUserId();

            var user = _context.UserModels.SingleOrDefault(c => c.UserId == userID);

            user.Experience += 25;

            double Level = Math.Max(Math.Floor(8.75 * Math.Log(25 + 100) + -40), 1);

            _context.SaveChanges();

      


            return View("Index", user);
        }
    }
}