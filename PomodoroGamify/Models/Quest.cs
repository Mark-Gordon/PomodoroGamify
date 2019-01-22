using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PomodoroGamify.Models
{
    public class Quest
    {
        public string Id { get; set; }
        public string QuestName { get; set; }
        public string QuestDescription { get; set; }
        public int LevelRequirement { get; set; }
        public int NumberOfPomodorosToComplete { get; set; }
        public int RewardExperience { get; set; }
    }
}