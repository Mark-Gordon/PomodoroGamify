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

            var user = _context.UserModels.SingleOrDefault(c => c.Id == userID);

            return View(user);
        }

        [HttpGet]
        public ActionResult AjaxGet()
        {
            System.Diagnostics.Debug.WriteLine("\n\n\n\n\nSTART GET\n\n\n");

            string userID = User.Identity.GetUserId();

            var user = _context.UserModels.SingleOrDefault(c => c.Id == userID);

            user.Experience += 25;

            user.Level = Convert.ToInt32(Math.Max(Math.Floor(8.75 * Math.Log(user.Experience + 100) + -40), 1));

            user.ExperienceOfCurrentLevel = Convert.ToInt32(user.GetExperienceToLevel(user.Level));

            user.ExperienceOfNextLevel = Convert.ToInt32(user.GetExperienceToLevel(user.Level + 1));



            _context.SaveChanges();

            var updatedUserData = new { Experience = user.Experience, Level = user.Level, PercentageToLevel = user.getPercentageToLevel(), ExperienceToLevelUp = user.GetExperienceToLevelUp() };
            System.Diagnostics.Debug.WriteLine(updatedUserData);

            System.Diagnostics.Debug.WriteLine("\n\n\n\n\nFINISH GET\n\n\n");
            return Json(updatedUserData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateQuest(string questName)
        {

            string userID = User.Identity.GetUserId();
            System.Diagnostics.Debug.WriteLine("\n\n\n\n\npsotWORK\n\n\n");
            System.Diagnostics.Debug.WriteLine(questName);
            var questID = _context.Quests.SingleOrDefault(c => c.QuestName == questName).Id;
            System.Diagnostics.Debug.WriteLine(questID);
            System.Diagnostics.Debug.WriteLine(userID);
            var questProgress = _context.UserQuestProgress.SingleOrDefault(c => c.Id == (userID + "-" + questID));
            questProgress.ProgressPomodoros = questProgress.ProgressPomodoros + 1;

            _context.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxPost(string id)
        {
            System.Diagnostics.Debug.WriteLine("\n\n\n\n\npsotWORK\n\n\n");
            int rating = Int32.Parse(id);

            string userID = User.Identity.GetUserId();
            
            var user = _context.UserModels.Include(c => c.Effective).Include(c => c.Pomodoro).SingleOrDefault(c => c.Id == userID);

            user.Pomodoro.NumberOfPomodoros = user.Pomodoro.NumberOfPomodoros + 1;
            double averageRating = (((user.Effective.AverageEffectiveRating * (user.Pomodoro.NumberOfPomodoros - 1)) + rating) / user.Pomodoro.NumberOfPomodoros);

            user.Effective.AverageEffectiveRating = averageRating;

            _context.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxPostHappy(string id)
        {

            int rating = Int32.Parse(id);

            string userID = User.Identity.GetUserId();

            var user = _context.UserModels.Include(c => c.Enjoyment).Include(c => c.Pomodoro).SingleOrDefault(c => c.Id == userID);

            double averageRating = (((user.Enjoyment.AverageEnjoymentRating * (user.Pomodoro.NumberOfPomodoros - 1)) + rating) / user.Pomodoro.NumberOfPomodoros);

            user.Enjoyment.AverageEnjoymentRating = averageRating;

            _context.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FailedPomodoro()
        {
           string userID = User.Identity.GetUserId();
            
            var user = _context.UserModels.Include(c => c.Pomodoro).SingleOrDefault(c => c.Id == userID);

            user.Pomodoro.NumberOfFailedPomodos = user.Pomodoro.NumberOfFailedPomodos + 1;

            _context.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public ActionResult GetQuests()
        {
            System.Diagnostics.Debug.WriteLine("\n\n\n\n\nQUESTS!\n\n\n");

            string userID = User.Identity.GetUserId();

            var user = _context.UserModels.Include(por => por.userQuestProgresses).Single(cat => cat.Id == userID);

            string[][] questArray = new string[_context.Quests.Count()][];

            
            int i = 0;

            foreach (UserQuestProgress a in user.userQuestProgresses)
            {
                System.Diagnostics.Debug.WriteLine("---------");
                var quest = _context.Quests.Single(cat => cat.Id == a.QuestId);
                System.Diagnostics.Debug.WriteLine(quest.Id);
                System.Diagnostics.Debug.WriteLine(quest.QuestName);

                System.Diagnostics.Debug.WriteLine(quest.QuestDescription);
                System.Diagnostics.Debug.WriteLine(quest.LevelRequirement);
                System.Diagnostics.Debug.WriteLine(quest.NumberOfPomodorosToComplete);
                System.Diagnostics.Debug.WriteLine(quest.RewardExperience);
                System.Diagnostics.Debug.WriteLine(a.ProgressPomodoros);


                questArray[i] = new string[] {a.QuestId, quest.QuestName, quest.QuestDescription, quest.LevelRequirement.ToString(),
                quest.NumberOfPomodorosToComplete.ToString(), quest.RewardExperience.ToString(), a.ProgressPomodoros.ToString()};
                i++;

            }



            return Json(questArray, JsonRequestBehavior.AllowGet);
        }




    }
}