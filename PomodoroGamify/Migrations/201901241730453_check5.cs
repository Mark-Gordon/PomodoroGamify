namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class check5 : DbMigration
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
                        UserModelId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserModels", t => t.UserModelId)
                .Index(t => t.UserModelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserQuestProgresses", "UserModelId", "dbo.UserModels");
            DropIndex("dbo.UserQuestProgresses", new[] { "UserModelId" });
            DropTable("dbo.UserQuestProgresses");
        }
    }
}
