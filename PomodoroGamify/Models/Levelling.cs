using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PomodoroGamify.Models
{
    public class Levelling
    {
        public string Id { get; set; }

        public int Experience { get; set; }

        public int Level { get; set; } = 1;

        public int ExperienceOfNextLevel { get; set; }

        public int ExperienceOfCurrentLevel { get; set; }
    }
}