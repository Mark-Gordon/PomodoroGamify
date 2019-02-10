namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixQuests30 : DbMigration
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
                        UserModelId = c.String(),
                        UserModel_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserModels", t => t.UserModel_UserId)
                .Index(t => t.UserModel_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserQuestProgresses", "UserModel_UserId", "dbo.UserModels");
            DropIndex("dbo.UserQuestProgresses", new[] { "UserModel_UserId" });
            DropTable("dbo.UserQuestProgresses");
        }
    }
}
