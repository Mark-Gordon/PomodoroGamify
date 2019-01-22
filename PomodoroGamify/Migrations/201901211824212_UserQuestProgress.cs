namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserQuestProgress : DbMigration
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
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserModels", "UserQuestProgress_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserModels", "UserQuestProgress_Id");
            AddForeignKey("dbo.UserModels", "UserQuestProgress_Id", "dbo.UserQuestProgresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserModels", "UserQuestProgress_Id", "dbo.UserQuestProgresses");
            DropIndex("dbo.UserModels", new[] { "UserQuestProgress_Id" });
            DropColumn("dbo.UserModels", "UserQuestProgress_Id");
            DropTable("dbo.UserQuestProgresses");
        }
    }
}
