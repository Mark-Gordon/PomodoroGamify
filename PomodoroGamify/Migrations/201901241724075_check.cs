namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class check : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserModels", name: "UserId", newName: "Id");
            RenameIndex(table: "dbo.UserModels", name: "IX_UserId", newName: "IX_Id");
            CreateTable(
                "dbo.UserQuestProgresses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        QuestId = c.String(),
                        ProgressPomodoros = c.Int(nullable: false),
                        UserModelID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserModels", t => t.UserModelID)
                .Index(t => t.UserModelID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserQuestProgresses", "UserModelID", "dbo.UserModels");
            DropIndex("dbo.UserQuestProgresses", new[] { "UserModelID" });
            DropTable("dbo.UserQuestProgresses");
            RenameIndex(table: "dbo.UserModels", name: "IX_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.UserModels", name: "Id", newName: "UserId");
        }
    }
}
