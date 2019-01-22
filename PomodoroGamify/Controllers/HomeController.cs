using PomodoroGamify.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Diagnostics;



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

        [HttpGet]
        public ActionResult AjaxGet()
        {
            string userID = User.Identity.GetUserId();

            var user = _context.UserModels.SingleOrDefault(c => c.UserId == userID);

            user.Experience += 25;

            user.Level = Convert.ToInt32(Math.Max(Math.Floor(8.75 * Math.Log(user.Experience + 100) + -40), 1));

            user.ExperienceOfCurrentLevel = Convert.ToInt32(user.GetExperienceToLevel(user.Level));

            user.ExperienceOfNextLevel = Convert.ToInt32(user.GetExperienceToLevel(user.Level + 1));



            _context.SaveChanges();

            var updatedUserData = new { Experience = user.Experience, Level = user.Level, PercentageToLevel = user.getPercentageToLevel(), ExperienceToLevelUp = user.GetExperienceToLevelUp() };


            return Json(updatedUserData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxPost(string id)
        {
            System.Diagnostics.Debug.WriteLine("\n\n\n\n\npsotWORK\n\n\n");
            int rating = Int32.Parse(id);

            string userID = User.Identity.GetUserId();
            
            var user = _context.UserModels.Include(c => c.Effective).Include(c => c.Pomodoro).SingleOrDefault(c => c.UserId == userID);

            user.Pomodoro.NumberOfPomodoros = user.Pomodoro.NumberOfPomodoros + 1;
            double averageRating = (((user.Effective.AverageEffectiveRating * (user.Pomodoro.NumberOfPomodoros - 1)) + rating) / user.Pomodoro.NumberOfPomodoros);

            user.Effective.AverageEffectiveRating = averageRating;

            _context.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FailedPomodoro()
        {
           string userID = User.Identity.GetUserId();
            
            var user = _context.UserModels.Include(c => c.Pomodoro).SingleOrDefault(c => c.UserId == userID);
                
           


            user.Pomodoro.NumberOfFailedPomodos = user.Pomodoro.NumberOfFailedPomodos + 1;

            _context.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult GetQuests()
        {
            System.Diagnostics.Debug.WriteLine("\n\n\n\n\nQUESTS!\n\n\n");

            string userID = User.Identity.GetUserId();

            var user = _context.UserModels.SingleOrDefault(c => c.UserId == userID);

            user.Experience += 25;

            user.Level = Convert.ToInt32(Math.Max(Math.Floor(8.75 * Math.Log(user.Experience + 100) + -40), 1));

            user.ExperienceOfCurrentLevel = Convert.ToInt32(user.GetExperienceToLevel(user.Level));

            user.ExperienceOfNextLevel = Convert.ToInt32(user.GetExperienceToLevel(user.Level + 1));


            System.Diagnostics.Debug.WriteLine(user.ApplicationUser.Email);


            _context.SaveChanges();

            var updatedUserData = new { Experience = user.Experience, Level = user.Level, PercentageToLevel = user.getPercentageToLevel(), ExperienceToLevelUp = user.GetExperienceToLevelUp() };


            return Json(updatedUserData, JsonRequestBehavior.AllowGet);
        }




    }
}