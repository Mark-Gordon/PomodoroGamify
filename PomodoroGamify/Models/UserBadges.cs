using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PomodoroGamify.Models
{
    public class UserBadges
    {
        public string Id { get; set; }
        public int BronzeBadges { get; set; }
        public int SilverBadges { get; set; }
        public int GoldBadges { get; set; }
    }
}