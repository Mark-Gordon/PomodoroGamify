namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRewardExperience : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quests", "RewardExperience", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quests", "RewardExperience");
        }
    }
}
