namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixQuests50 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserQuestProgresses", "UserModelID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserQuestProgresses", "UserModelID");
        }
    }
}
