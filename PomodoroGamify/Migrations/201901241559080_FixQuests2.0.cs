namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixQuests20 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserQuestProgresses", "UserModel_UserId", "dbo.UserModels");
            DropIndex("dbo.UserQuestProgresses", new[] { "UserModel_UserId" });
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
                        UserModelId = c.Int(nullable: false),
                        UserModel_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UserQuestProgresses", "UserModel_UserId");
            AddForeignKey("dbo.UserQuestProgresses", "UserModel_UserId", "dbo.UserModels", "UserId");
        }
    }
}
