namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixQuests80 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserQuestProgresses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        QuestId = c.String(),
                        ProgressPomodoros = c.Int(nullable: false),
                        UserModelID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserQuestProgresses");
        }
    }
}
