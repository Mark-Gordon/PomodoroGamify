namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixQuests10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserModels", "UserQuestProgress_Id", "dbo.UserQuestProgresses");
            DropIndex("dbo.UserModels", new[] { "UserQuestProgress_Id" });
            AddColumn("dbo.UserQuestProgresses", "UserModelId", c => c.Int(nullable: false));
            AddColumn("dbo.UserQuestProgresses", "UserModel_UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserQuestProgresses", "UserModel_UserId");
            AddForeignKey("dbo.UserQuestProgresses", "UserModel_UserId", "dbo.UserModels", "UserId");
            DropColumn("dbo.UserModels", "UserQuestProgress_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserModels", "UserQuestProgress_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.UserQuestProgresses", "UserModel_UserId", "dbo.UserModels");
            DropIndex("dbo.UserQuestProgresses", new[] { "UserModel_UserId" });
            DropColumn("dbo.UserQuestProgresses", "UserModel_UserId");
            DropColumn("dbo.UserQuestProgresses", "UserModelId");
            CreateIndex("dbo.UserModels", "UserQuestProgress_Id");
            AddForeignKey("dbo.UserModels", "UserQuestProgress_Id", "dbo.UserQuestProgresses", "Id");
        }
    }
}
