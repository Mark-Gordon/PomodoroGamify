using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PomodoroGamify.Models
{
    public class Pomodoro
    {
        public string Id { get; set; }
        public int NumberOfPomodoros { get; set; }
        public int NumberOfFailedPomodos { get; set; }
    }
}