namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLevelToUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserModels", "Level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserModels", "Level");
        }
    }
}
