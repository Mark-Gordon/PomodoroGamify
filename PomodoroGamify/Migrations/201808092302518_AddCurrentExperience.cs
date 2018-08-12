namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrentExperience : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserModels", "ExperienceOfCurrentLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserModels", "ExperienceOfCurrentLevel");
        }
    }
}
