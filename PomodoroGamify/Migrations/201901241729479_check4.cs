namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class check4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserQuestProgresses", "UserModelId", "dbo.UserModels");
            DropIndex("dbo.UserQuestProgresses", new[] { "UserModelId" });
            DropTable("dbo.UserQuestProgresses");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UserQuestProgresses", "UserModelId");
            AddForeignKey("dbo.UserQuestProgresses", "UserModelId", "dbo.UserModels", "Id");
        }
    }
}
