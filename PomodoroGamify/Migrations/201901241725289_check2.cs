namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class check2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserQuestProgresses", new[] { "UserModelID" });
            CreateIndex("dbo.UserQuestProgresses", "UserModelId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserQuestProgresses", new[] { "UserModelId" });
            CreateIndex("dbo.UserQuestProgresses", "UserModelID");
        }
    }
}
