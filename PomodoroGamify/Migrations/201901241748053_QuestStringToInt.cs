namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestStringToInt : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserQuestProgresses");
            AlterColumn("dbo.UserQuestProgresses", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserQuestProgresses", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserQuestProgresses");
            AlterColumn("dbo.UserQuestProgresses", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserQuestProgresses", "Id");
        }
    }
}
