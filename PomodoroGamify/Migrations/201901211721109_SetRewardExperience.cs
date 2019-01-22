namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetRewardExperience : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Quests SET RewardExperience = 60 WHERE Id = 0");
            Sql("UPDATE Quests SET RewardExperience = 100 WHERE Id = 1");
            Sql("UPDATE Quests SET RewardExperience = 150 WHERE Id = 2");
            Sql("UPDATE Quests SET RewardExperience = 250 WHERE Id = 3");
            Sql("UPDATE Quests SET RewardExperience = 350 WHERE Id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
