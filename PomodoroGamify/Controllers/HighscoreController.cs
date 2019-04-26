using PomodoroGamify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Data.Entity;

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

            var users = _context.UserModels.Include(c => c.Levelling).ToList();

            return View(users);
        }

        public ActionResult PomodoroCompleted()
        {

            string userID = User.Identity.GetUserId();

            var user = _context.UserModels.Include(c => c.Levelling).SingleOrDefault(c => c.Id == userID);

            user.Levelling.Experience += 25;

            double Level = Math.Max(Math.Floor(8.75 * Math.Log(25 + 100) + -40), 1);

            _context.SaveChanges();

      


            return View("Index", user);
        }
    }
}