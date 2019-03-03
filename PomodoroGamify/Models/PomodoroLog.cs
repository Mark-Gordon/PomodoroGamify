using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PomodoroGamify.Models
{
    public class PomodoroLog
    {
        public int Id { get; set; }
        public DateTime DateCompleted { get; set; }

        public string UserModelId { get; set; }
        public UserModel User { get; set; }

    }
}