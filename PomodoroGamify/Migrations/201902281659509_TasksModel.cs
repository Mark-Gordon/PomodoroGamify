namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TasksModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TaskName = c.String(),
                        TaskDescription = c.String(),
                        NumberOfPomodorosToComplete = c.Int(nullable: false),
                        RewardExperience = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserTaskProgresses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TaskId = c.String(maxLength: 128),
                        ProgressPomodoros = c.Int(nullable: false),
                        UserModelId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskModels", t => t.TaskId)
                .ForeignKey("dbo.UserModels", t => t.UserModelId)
                .Index(t => t.TaskId)
                .Index(t => t.UserModelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTaskProgresses", "UserModelId", "dbo.UserModels");
            DropForeignKey("dbo.UserTaskProgresses", "TaskId", "dbo.TaskModels");
            DropIndex("dbo.UserTaskProgresses", new[] { "UserModelId" });
            DropIndex("dbo.UserTaskProgresses", new[] { "TaskId" });
            DropTable("dbo.UserTaskProgresses");
            DropTable("dbo.TaskModels");
        }
    }
}
