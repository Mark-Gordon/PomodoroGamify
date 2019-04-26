using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PomodoroGamify.Models
{
    public class UserModel
    {

        [Key, ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public Levelling Levelling { get; set; }
        public string LevellingID { get; set; }

        public Pomodoro Pomodoro { get; set; }
        public string PomodoroId { get; set; }



        public Effective Effective { get; set; }
        public string EffectiveID { get; set; }

        public Enjoyment Enjoyment { get; set; }
        public string EnjoymentID { get; set; }

        public UserBadges UserBadges { get; set; }
        public string UserBadgesId { get; set; }

        public IList<UserQuestProgress> userQuestProgresses { get; set; }

        public IList<PomodoroLog> PomodoroLog { get; set; }




        public virtual ApplicationUser ApplicationUser
        {
            get; set;

        }


        public double GetExperienceToLevelUp()
        {
            return (Math.Ceiling(Math.Exp(((Levelling.Level + 1) + 40) / 8.75)) - 100) - Levelling.Experience;
        }

        public double GetExperienceToLevel(int lev)
        {
            return (Math.Ceiling(Math.Exp((lev + 40) / 8.75)) - 100);
        }

        public int getPercentageToLevel()
        {
            if ((Levelling.ExperienceOfNextLevel - Levelling.ExperienceOfCurrentLevel) == 0)
            {
                return 0;
            }
            else
            {
                return ((Levelling.Experience - Levelling.ExperienceOfCurrentLevel) * 100) / (Levelling.ExperienceOfNextLevel - Levelling.ExperienceOfCurrentLevel);
            }
        }

        public int getLevelFromExperience(int exp)
        {
            return Convert.ToInt32(Math.Max(Math.Floor(8.75 * Math.Log(exp + 100) + -40), 1));
        }
    }
}