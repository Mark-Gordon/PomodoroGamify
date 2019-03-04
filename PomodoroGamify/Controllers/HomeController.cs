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

        public int RewardTasks()
        {
            var rewardExperience = 0;
            string userID = User.Identity.GetUserId();
            DateTime DateToday = DateTime.Today.AddDays(-1);
            DateTime SevenDaysAgo = DateTime.Today.AddDays(-7);
            DateTime DateThisWeek = DateToday.AddDays(-(int)DateToday.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime DateThisMonth = new DateTime(DateToday.Year, DateToday.Month, 1);

            var pomCountToday = _context.PomodoroLog.Where(c => c.UserModelId == userID).Where(c => c.DateCompleted > SevenDaysAgo).Count();
            var pomCountThisWeek = _context.PomodoroLog.Where(c => c.UserModelId == userID).Where(c => c.DateCompleted > DateThisWeek).Count();
            var pomCountThisMonth = _context.PomodoroLog.Where(c => c.UserModelId == userID).Where(c => c.DateCompleted > DateThisMonth).Count();


            var user = _context.UserModels.Include(c => c.UserBadges).Single(c => c.Id == userID);

            if (pomCountToday == 9)
            {
                rewardExperience += 50;
                user.UserBadges.BronzeBadges += 1;
            }
            System.Diagnostics.Debug.WriteLine("POM COUNT: " + pomCountThisWeek);
            if (pomCountThisWeek == 49) {
                System.Diagnostics.Debug.WriteLine("IN HERE");
                rewardExperience += 250;
                user.UserBadges.SilverBadges += 1;
            }
            if(pomCountThisMonth == 199){
                rewardExperience += 500;
                user.UserBadges.GoldBadges += 1;
            }

            return rewardExperience;

        }

        [HttpPost]
        public ActionResult PomodoroCompleted(string questName)
        {

            if(questName != "")
            {
                UpdateQuest(questName);
            }

            

            string userID = User.Identity.GetUserId();

            var user = _context.UserModels.SingleOrDefault(c => c.Id == userID);

            int taskRewardExperience = RewardTasks();
            user.Experience += taskRewardExperience;

            if (questName.Equals(""))
            {
                user.Experience += 25;


            }
            
            user.Level = Convert.ToInt32(Math.Max(Math.Floor(8.75 * Math.Log(user.Experience + 100) + -40), 1));

            user.ExperienceOfCurrentLevel = Convert.ToInt32(user.GetExperienceToLevel(user.Level));

            user.ExperienceOfNextLevel = Convert.ToInt32(user.GetExperienceToLevel(user.Level + 1));

            var pomLog = new PomodoroLog
            {
                DateCompleted = DateTime.Now,
                UserModelId = user.Id
            };

            _context.PomodoroLog.Add(pomLog);




            _context.SaveChanges();

            System.Diagnostics.Debug.WriteLine("HERE " + user.Experience + " -> " + user.Level + " -> " + user.getPercentageToLevel());


            var updatedUserData = new { Experience = user.Experience, Level = user.Level, PercentageToLevel = user.getPercentageToLevel(), ExperienceToLevelUp = user.GetExperienceToLevelUp() };


            return Json(updatedUserData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetUpdateQuestData(string questName)
        {

            string userID = User.Identity.GetUserId();
            var quest = _context.Quests.SingleOrDefault(c => c.QuestName == questName);
            var questID = quest.Id;
            var questProgress = _context.UserQuestProgress.SingleOrDefault(c => c.Id == (userID + "-" + questID));

            var updatedQuestProgress = new { ProgressPomodoros = questProgress.ProgressPomodoros, PomodorosToComplete = quest.NumberOfPomodorosToComplete };

            return Json(updatedQuestProgress, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateQuest(string questName)
        {

            string userID = User.Identity.GetUserId();

            var quest = _context.Quests.SingleOrDefault(c => c.QuestName == questName);
            var questID = quest.Id;
            var questProgress = _context.UserQuestProgress.SingleOrDefault(c => c.Id == (userID + "-" + questID));
            questProgress.ProgressPomodoros = questProgress.ProgressPomodoros + 1;

            _context.SaveChanges();
           
            if (questProgress.ProgressPomodoros == quest.NumberOfPomodorosToComplete)
            {
                RewardQuest(questName);
            }

            var updatedQuestProgress = new { ProgressPomodoros = questProgress.ProgressPomodoros, PomodorosToComplete =  quest.NumberOfPomodorosToComplete};

            return Json(updatedQuestProgress, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxPost(string id)
        {

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
        public ActionResult RewardQuest(string questName)
        {
            string userID = User.Identity.GetUserId();
            var user = _context.UserModels.SingleOrDefault(c => c.Id == userID);


            var quest = _context.Quests.SingleOrDefault(c => c.QuestName == questName);
            var rewardExerpience = quest.RewardExperience;


            user.Experience += rewardExerpience;

            user.Level = Convert.ToInt32(Math.Max(Math.Floor(8.75 * Math.Log(user.Experience + 100) + -40), 1));

            user.ExperienceOfCurrentLevel = Convert.ToInt32(user.GetExperienceToLevel(user.Level));

            user.ExperienceOfNextLevel = Convert.ToInt32(user.GetExperienceToLevel(user.Level + 1));



            _context.SaveChanges();

            var updatedUserData = new { Experience = user.Experience, Level = user.Level, PercentageToLevel = user.getPercentageToLevel(), ExperienceToLevelUp = user.GetExperienceToLevelUp(), QuestReward = quest.RewardExperience };

            return Json(updatedUserData, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetBadges()
        {

            string userID = User.Identity.GetUserId();


            var user = _context.UserModels.Include(c => c.UserBadges).Single(c => c.Id == userID);



            var userBadges = new { bronzeBadges = user.UserBadges.BronzeBadges, silverBadges = user.UserBadges.SilverBadges, goldBadges = user.UserBadges.GoldBadges };



            return Json(userBadges, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTasks()
        {


            string userID = User.Identity.GetUserId();

            DateTime DateToday = DateTime.Today.AddDays(-1);
            DateTime SevenDaysAgo = DateTime.Today.AddDays(-7);
            DateTime DateThisWeek = DateToday.AddDays(-(int)DateToday.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime DateThisMonth = new DateTime(DateToday.Year, DateToday.Month, 1);

            var pomCountToday = _context.PomodoroLog.Where(c => c.UserModelId == userID).Where(c => c.DateCompleted > SevenDaysAgo).Count();
            var pomCountThisWeek = _context.PomodoroLog.Where(c => c.UserModelId == userID).Where(c => c.DateCompleted > DateThisWeek).Count();
            var pomCountThisMonth = _context.PomodoroLog.Where(c => c.UserModelId == userID).Where(c => c.DateCompleted > DateThisMonth).Count();

            var pomCount = new { PomsToday = pomCountToday, PomsThisWeek = pomCountThisWeek, pomCountThisMonth = pomCountThisMonth};

            int[] pomCountArr = new int[3] { pomCountToday , pomCountThisWeek , pomCountThisMonth };


            return Json(pomCountArr, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetQuests()
        {


            string userID = User.Identity.GetUserId();

            var user = _context.UserModels.Include(por => por.userQuestProgresses).Single(cat => cat.Id == userID);

            string[][] questArray = new string[_context.Quests.Count()][];

            
            int i = 0;

            foreach (UserQuestProgress a in user.userQuestProgresses)
            {

                var quest = _context.Quests.Single(cat => cat.Id == a.QuestId);



                questArray[i] = new string[] {a.QuestId, quest.QuestName, quest.QuestDescription, quest.LevelRequirement.ToString(),
                quest.NumberOfPomodorosToComplete.ToString(), quest.RewardExperience.ToString(), a.ProgressPomodoros.ToString()};
                i++;

            }



            return Json(questArray, JsonRequestBehavior.AllowGet);
        }




    }
}