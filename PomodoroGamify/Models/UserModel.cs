using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PomodoroGamify.Models
{
    public class UserModel
    {

        [Key, ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public int Experience { get; set; }

        public int Level { get; set; } = 1;

        public int ExperienceOfNextLevel { get; set; }

        public int ExperienceOfCurrentLevel { get; set; }

        public virtual ApplicationUser ApplicationUser
        {
            get; set;

        }

        public double GetExperienceToLevelUp()
        {
            return (Math.Ceiling(Math.Exp(((Level + 1) + 40) / 8.75)) - 100) - Experience;
        }

        public double GetExperienceToLevel(int lev)
        {
            return (Math.Ceiling(Math.Exp((lev + 40) / 8.75)) - 100);
        }

        public int getPercentageToLevel()
        {
            if ((ExperienceOfNextLevel - ExperienceOfCurrentLevel) == 0)
            {
                return 0;
            }
            else
            {
                return ((Experience - ExperienceOfCurrentLevel) * 100) / (ExperienceOfNextLevel - ExperienceOfCurrentLevel);
            }
        }

        public int getLevelFromExperience(int exp)
        {
            return Convert.ToInt32(Math.Max(Math.Floor(8.75 * Math.Log(exp + 100) + -40), 1));
        }
    }
}