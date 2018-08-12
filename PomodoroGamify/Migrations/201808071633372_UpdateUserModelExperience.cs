namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserModelExperience : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserModels", "Experience", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserModels", "Experience");
        }
    }
}
