namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PomodoroLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCompleted = c.String(),
                        UserModelId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserModels", t => t.UserModelId)
                .Index(t => t.UserModelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PomodoroLogs", "UserModelId", "dbo.UserModels");
            DropIndex("dbo.PomodoroLogs", new[] { "UserModelId" });
            DropTable("dbo.PomodoroLogs");
        }
    }
}
