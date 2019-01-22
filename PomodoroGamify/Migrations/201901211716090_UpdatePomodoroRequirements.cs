namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePomodoroRequirements : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Quests SET NumberOfPomodorosToComplete = 2 WHERE Id = 0");
            Sql("UPDATE Quests SET NumberOfPomodorosToComplete = 3 WHERE Id = 1");
            Sql("UPDATE Quests SET NumberOfPomodorosToComplete = 5 WHERE Id = 2");
            Sql("UPDATE Quests SET NumberOfPomodorosToComplete = 8 WHERE Id = 3");
            Sql("UPDATE Quests SET NumberOfPomodorosToComplete = 10 WHERE Id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
