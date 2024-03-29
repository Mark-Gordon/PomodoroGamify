﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PomodoroGamify.Models
{
    public class UserQuestProgress
    {

        public string Id { get; set; }
        public string QuestId { get; set;}
        public Quest Quest { get; set; }
        public int ProgressPomodoros { get; set; }

        public string UserModelId { get; set; }
        public UserModel User { get; set; }
    }
}