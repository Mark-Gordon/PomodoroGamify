namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixQuests40 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserQuestProgresses", "UserModelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserQuestProgresses", "UserModelId", c => c.String());
        }
    }
}
