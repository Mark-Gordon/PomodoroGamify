using PomodoroGamify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace PomodoroGamify.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
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

            user.Level = Convert.ToInt32(Math.Max(Math.Floor(8.75 * Math.Log(user.Experience + 100) + -40), 1));

            user.ExperienceOfCurrentLevel = Convert.ToInt32(user.GetExperienceToLevel(user.Level));

            user.ExperienceOfNextLevel = Convert.ToInt32(user.GetExperienceToLevel(user.Level+1));



            _context.SaveChanges();




            return View("Index", user);
        }
    }
}