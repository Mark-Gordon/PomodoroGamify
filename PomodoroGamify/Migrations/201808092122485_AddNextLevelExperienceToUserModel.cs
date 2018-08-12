namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNextLevelExperienceToUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserModels", "ExperienceOfNextLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserModels", "ExperienceOfNextLevel");
        }
    }
}
